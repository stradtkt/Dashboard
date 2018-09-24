using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dashboard.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;


namespace Dashboard.Controllers
{
    public class HomeController : Controller
    {
        private DashContext _dContext;
        public HomeController(DashContext context)
        {
            _dContext = context;
        }
        private User ActiveUser 
        {
            get 
            {
                return _dContext.users.Where(u => u.user_id == HttpContext.Session.GetInt32("user_id")).FirstOrDefault();
            }
        }
        [HttpGet("")]
        public IActionResult Register()
        {
            ViewBag.user = ActiveUser;
            return View();
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            ViewBag.user = ActiveUser;
            return View();
        }

        [HttpPost("registeruser")]
        public IActionResult RegisterUser(RegisterUser newuser)
        {
            User CheckEmail = _dContext.users
                .Where(u => u.email == newuser.email)
                .SingleOrDefault();

            if(CheckEmail != null)
            {
                ViewBag.errors = "That email already exists";
                return RedirectToAction("Register");
            }
            if(ModelState.IsValid)
            {
                PasswordHasher<RegisterUser> Hasher = new PasswordHasher<RegisterUser>();
                User newUser = new User
                {
                    user_id = newuser.user_id,
                    first_name = newuser.first_name,
                    last_name = newuser.last_name,
                    email = newuser.email,
                    address = newuser.address,
                    city = newuser.city,
                    state = newuser.state,
                    zip = newuser.zip,
                    phone = newuser.phone,
                    password = Hasher.HashPassword(newuser, newuser.password)
                  };
                _dContext.Add(newUser);
                _dContext.SaveChanges();
                ViewBag.success = "Successfully registered";
                return RedirectToAction("Login");
            }
            else
            {
                return View("Register");
            }
        }

        [HttpPost("loginuser")]
        public IActionResult LoginUser(LoginUser loginUser) 
        {
            User CheckEmail = _dContext.users
                .SingleOrDefault(u => u.email == loginUser.email);
            if(CheckEmail != null)
            {
                var Hasher = new PasswordHasher<User>();
                if(0 != Hasher.VerifyHashedPassword(CheckEmail, CheckEmail.password, loginUser.password))
                {
                    HttpContext.Session.SetInt32("user_id", CheckEmail.user_id);
                    HttpContext.Session.SetString("first_name", CheckEmail.first_name);
                    return RedirectToAction("Dashboard");
                }
                else
                {
                    ViewBag.errors = "Incorrect Password";
                    return View("Register");
                }
            }
            else
            {
                ViewBag.errors = "Email not registered";
                return View("Register");
            }
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
//*************************************Dashboard************************************************* */

        [HttpGet("Dashboard")]
        public IActionResult Dashboard()
        {
            if(ActiveUser == null)
            {
                return RedirectToAction("Login");
            }
            List<User> users = _dContext.users.ToList();
            List<Product> products = _dContext.products.ToList();
            List<Order> orders = _dContext.orders.ToList();
            List<Comment> comments = _dContext.comments.ToList();
            List<Message> messages = _dContext.messages.ToList();
            ViewBag.comments = comments;
            ViewBag.messages = messages;
            ViewBag.orders = orders;
            ViewBag.products = products;
            ViewBag.users = users;
            ViewBag.user = ActiveUser;
            return View();
        }
//***********************************Products******************************************* */
        [HttpGet("AddProduct")]
        public IActionResult AddProduct()
        {
            if(ActiveUser == null)
            {
                return RedirectToAction("Login");
            }
            ViewBag.user = ActiveUser;
            return View();
        }
        [HttpGet("Products")]
        public IActionResult Products()
        {
            if(ActiveUser == null)
            {
                return RedirectToAction("Login");
            }
            List<Product> products = _dContext.products.ToList();
            ViewBag.products = products;
            ViewBag.user = ActiveUser;
            return View();
        }
        [HttpPost("ProcessProduct")]
        public IActionResult ProcessProduct(Product prod)
        {
            if(ActiveUser == null)
            {
                return RedirectToAction("Login");
            }
            if(ModelState.IsValid)
            {
                Product newProduct = new Product
                {
                    name = prod.name,
                    short_desc = prod.short_desc,
                    desc = prod.desc,
                    image = prod.image,
                    price = prod.price,
                    weight = prod.weight,
                    qty = prod.qty
                };
                _dContext.products.Add(newProduct);
                _dContext.SaveChanges();
                return RedirectToAction("Products");
            }
            return View("AddProduct");
        }
        [HttpGet("DeleteProduct/{product_id}")]
        public IActionResult DeleteProduct(int product_id)
        {
            Product toDelete = _dContext.products.Where(p => p.product_id == product_id).SingleOrDefault();
            _dContext.products.Remove(toDelete);
            _dContext.SaveChanges();
            return RedirectToAction("Products");
        }
        [HttpGet("Product/{id}")]
        public IActionResult Product(int id)
        {
            Product product = _dContext.products
                .Include(c => c.ProductsCategories)
                .ThenInclude(cat => cat.Categories)
                .SingleOrDefault(p => p.product_id == id);
            List<Category> categories = _dContext.categories.ToList();
            ViewBag.product = product;
            ViewBag.categories = categories;
            ViewBag.user = ActiveUser;
            return View();
        }

        [HttpPost("LinkProductToCategory")]
        public IActionResult LinkProductToCategory(int product_id, int category_id)
        {
            ProductsCategories newItem = new ProductsCategories
            {
                product_id = product_id,
                category_id = category_id
            };
            _dContext.products_has_categories.Add(newItem);
            _dContext.SaveChanges();
            return RedirectToAction("Products");
        }

//******************************************End Products****************************************************** */

//*******************************Users***************************************************** */

        [HttpGet("Users")]
        public IActionResult Users()
        {
            if(ActiveUser == null)
            {
                return RedirectToAction("Login");
            }
            List<User> users = _dContext.users
                .Include(m => m.Messages)
                .Include(c => c.Comments)
                .ToList();
            ViewBag.user = ActiveUser;
            ViewBag.users = users;
            return View();
        }

        [HttpGet("UserProfile/{user_id}")]
        public IActionResult UserProfile(int user_id) 
        {
            User user = _dContext.users
                .Include(m => m.Messages)
                .Include(c => c.Comments)
                .Where(u => u.user_id == user_id)
                .SingleOrDefault();
            ViewBag.user = user;
            return View();
        }
        [HttpGet("DeleteUser/{user_id}")]
        public IActionResult DeleteUser(int user_id) 
        {
            User user = _dContext.users.Where(u => u.user_id == user_id).SingleOrDefault();
            _dContext.users.Remove(user);
            _dContext.SaveChanges();
            return RedirectToAction("Users");
        }

        [HttpGet("EditUser/{user_id}")]
        public IActionResult EditUser(int user_id)
        {
            User user = _dContext.users.Where(u => u.user_id == user_id).SingleOrDefault();
            ViewBag.theUser = user;
            return View();
        }
        [Route("{user_id}/ProcessEditUser")]
        public IActionResult ProcessEditUser(int user_id, string first_name, string last_name, string address, string city, string state, string zip, string phone, string email)
        {
            User user = _dContext.users.Where(u => u.user_id == user_id).SingleOrDefault();
            user.first_name = first_name;
            user.last_name = last_name;
            user.address = address;
            user.city = city;
            user.state = state;
            user.zip = zip;
            user.phone = phone;
            user.email = email;
            _dContext.SaveChanges();
            return RedirectToAction("Users");
        }



//****************************************************Messages & Comments*************************************************************** */

        [HttpGet("MessageBoard")]
        public IActionResult MessageBoard()
        {
            if(ActiveUser == null)
            {
                return RedirectToAction("Login");
            }
            List<Message> messages = _dContext.messages
                .Include(u => u.Users)
                .Include(c => c.Comments)
                .ThenInclude(cu => cu.Users)
                .ToList();
            ViewBag.messages = messages;
            ViewBag.user = ActiveUser;
            return View();
        }
        [HttpGet("AddMessage")]
        public IActionResult AddMessage()
        {
            ViewBag.user = ActiveUser;
            return View();
        }
        [HttpPost("ProcessMessage")]
        public IActionResult ProcessMessage(Message mess)
        {
            if(ActiveUser == null)
            {
                return RedirectToAction("Login");
            }
            if(ModelState.IsValid)
            {
                Message msg = new Message
                {
                    user_id = ActiveUser.user_id,
                    message = mess.message
                };
                _dContext.messages.Add(msg);
                _dContext.SaveChanges();
                return RedirectToAction("MessageBoard");
            }
            return View("MessageBoard");
        }
        [HttpGet("Message/{message_id}")]
        public IActionResult Message(int message_id)
        {
            if(ActiveUser == null)
            {
                return RedirectToAction("Login");
            }
            Message message = _dContext.messages
                .Include(u => u.Users)
                .Include(c => c.Comments)
                .ThenInclude(cu => cu.Users)
                .Where(m => m.message_id == message_id)
                .SingleOrDefault();
            ViewBag.message = message;
            ViewBag.user = ActiveUser;
            return View();
        }

        [HttpPost("ProcessComment")]
        public IActionResult ProcessComment(int message_id, string comm)
        {
            Comment c = new Comment
            {
                message_id = message_id,
                comment = comm,
                user_id = ActiveUser.user_id
            };
            _dContext.comments.Add(c);
            _dContext.SaveChanges();
            return Redirect("/Message/"+ message_id);
        }

        [HttpGet("DeleteMessage/{message_id}")]
        public IActionResult DeleteMessage(int message_id)
        {
            if(ActiveUser == null)
            {
                return RedirectToAction("Login");
            }
            Message message = _dContext.messages.Where(m => m.message_id == message_id).SingleOrDefault();
            _dContext.messages.Remove(message);
            _dContext.SaveChanges();
            return RedirectToAction("MessageBoard");
        }
        [HttpGet("DeleteComment/{comment_id}")]
        public IActionResult DeleteComment(int comment_id)
        {
            if(ActiveUser == null)
            {
                return RedirectToAction("Login");
            }
            Comment comment = _dContext.comments.Where(c => c.comment_id == comment_id).SingleOrDefault();
            _dContext.comments.Remove(comment);
            _dContext.SaveChanges();
            return RedirectToAction("MessageBoard");
        }
//************************************************************************Orders**************************************************************** */

        [HttpGet("Orders")]
        public IActionResult Orders()
        {
            List<Order> orders = _dContext.orders
                .Include(u => u.Users)
                .Include(op => op.OrdersProducts)
                .ThenInclude(p => p.Products)
                .ToList();
            ViewBag.orders = orders;
            ViewBag.user = ActiveUser;
            return View();
        }

        [HttpGet("AddOrder")]
        public IActionResult AddOrder()
        {
            ViewBag.user = ActiveUser;
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
