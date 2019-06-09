using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using BookService.Models;

namespace BookService.Controllers
{
    [RequireHttps]
    public class PasswordsController : ApiController
    {
        private BookServiceContext db = new BookServiceContext();

        // GET: api/Passwords
        public IQueryable<Password> GetPasswords()
        {
            return db.Passwords;
        }
        
        // GET: api/passwords/recover/XX-XX-osv
        [System.Web.Http.Route("api/passwords/privatekey/{userInfo}")]
        [ResponseType(typeof(string))]
        public async Task<IHttpActionResult> GetPrivateKey(string userInfo)
        {
            var match = await db.Passwords.FirstOrDefaultAsync(p => p.UserInfo == userInfo);
            if (match == null)
            {
                return NotFound();
            }

            return Ok(match.PrivateKey);
        }

        // GET: api/passwords/generate/id
        // {MachineName}:{UserName}:{private key}:{public key}
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/passwords/save")]
        [ResponseType(typeof(Password))]
        public async Task<IHttpActionResult> PostPassword(Password password)
        {
            //var parts = userInfo.Split(';');

            password.Id = CreatePassword(15);
            password.TimeStamp = DateTime.UtcNow;

            //var password = new Password
            //{
            //    PublicKey = parts[3],
            //    PrivateKey = parts[2],
            //    UserInfo = parts[0] + ";" + parts[1],
            //    TimeStamp = DateTime.UtcNow
            //};

            var match = await db.Passwords.FirstOrDefaultAsync(p => p.UserInfo == password.UserInfo);
            if (match != null)
            {
                return Ok(match);
            }

            db.Passwords.Add(password);

            try
            {
                db.SaveChanges();
                //await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PasswordExists(password.PublicKey))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return Ok(password);
        }

        // GET: api/Passwords/dfxshdshtrsdhrtsdhx
        [ResponseType(typeof(Password))]
        public async Task<IHttpActionResult> GetPassword(string id)
        {
            var match = await db.Passwords.FindAsync(id);
            if (match == null)
            {
                return NotFound();
            }

            return Ok(match);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PasswordExists(string id)
        {
            return db.Passwords.Count(e => e.PublicKey == id) > 0;
        }
        
        public string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
    }
}