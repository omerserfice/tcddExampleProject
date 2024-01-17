using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using tcddExampleProject.Models;
using tcddExampleProject.Models.DTO;

namespace tcddExampleProject.Controllers
{
    public class UserController : Controller
    {
        private readonly ExampleDbContext _exampleDbContext;
        private readonly IMapper _mapper;

        public UserController(ExampleDbContext exampleDbContext,IMapper mapper)
        {
            _exampleDbContext = exampleDbContext;
            _mapper = mapper;
        }

        //kullanıcıların listelenmesi işlemi
        public IActionResult Index()
        {
            ICollection<User> userList = _exampleDbContext.Users.ToList();
            ICollection<GetAllUserDto> userDto =_mapper.Map<ICollection<GetAllUserDto>>(userList);
            string successMessage = TempData["SuccessMessage"] as string;
            ViewBag.SuccessMessage = successMessage;
            return View(userDto);
        }

        public IActionResult AddUser()
        {
            return View();
        }
        //kullanıcı eklemeden önce tc kimlik no sorgusu kpsden çekilip bilgiler doğru ise kayıt işleminin yapılması
        [HttpPost]
        public IActionResult AddUser(AddUserDto addUserDto)
        {

            DateTime unspecifiedDateTime = addUserDto.DogumTarihi.Value;
            DateTime utcDateTime = DateTime.SpecifyKind(unspecifiedDateTime, DateTimeKind.Utc);
            addUserDto.DogumTarihi = utcDateTime;
            
            

            ServiceKPS service = new ServiceKPS();
            Response result = new Response();
            result._parameters.TCKimlikNo = long.Parse(addUserDto.TcNo);
            result._parameters.Ad = addUserDto.Ad;
            result._parameters.Soyad = addUserDto.Soyad;
            result._parameters.DogumYili = GetYearFromDatetime(addUserDto.DogumTarihi.Value);

            // kimlik bilgisinin sorgulandığı kısım ve  dönen sonuç
            var res = service.OnGetService(result._parameters);

            if (res == null)
            {
                throw new Exception("Servis çekilemedi.");
            }
            else
            {
                if (res.Result == true)
                {
                    var newUser = _mapper.Map<User>(addUserDto);

                    _exampleDbContext.Users.Add(newUser);
                    _exampleDbContext.SaveChanges();
                    TempData["SuccessMessage"] = "Kullanıcı sorgulama işlemi başarılı bilgiler kaydedildi";
                    return RedirectToAction("Index");
                }
            }

            TempData["DangerMessage"] = "Kullanıcı sorgulama işlemi başarısız";
            string dangerMessage = TempData["DangerMessage"] as string;
            ViewBag.DangerMessage = dangerMessage;
            return View();
        }

        // datetime dan yıl bilgisini çekme
        public int GetYearFromDatetime(DateTime dateTime)
        {
            int year = 0;
            var tarih = dateTime.ToString();
            var splitDate = tarih.Split(' ')[0].Split(".")[2];
            year = int.Parse(splitDate);
            return year;
        }

    }
}
