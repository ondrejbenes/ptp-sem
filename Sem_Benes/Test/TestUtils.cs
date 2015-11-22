using Sem_Benes.API;
using Sem_Benes.Model;

namespace Sem_Benes.Test
{
    class TestUtils
    {
        public static void CreateTestUsersAndSaveToFile()
        {
            var userMemoryDao = UserMemoryDao.Get();
            var userService = new UserServiceImpl(userMemoryDao);

            User testUser1 = new User("Test", "User1", PasswordHash.PasswordHash.CreateHash("heslo"), UserRole.AppUser,
                "test1") {Id = 0};
            User testUser2 = new User("Test", "User2", PasswordHash.PasswordHash.CreateHash("heslo"), UserRole.AppUser,
                "test2") {Id = 1};
            User testAdmin = new User("Test", "Admin", PasswordHash.PasswordHash.CreateHash("admin"), UserRole.Admin,
                "admin") {Id = 2};

            userService.SaveUser(testUser1);
            userService.SaveUser(testUser2);
            userService.SaveUser(testAdmin);

            userMemoryDao.SaveToFile();
        }

        public static void CreateTestCompaniesAndSaveToFile()
        {
            var companyMemoryDao = CompanyMemoryDao.Get();
            var companyService = new CompanyServiceImpl(companyMemoryDao);

            var testCompany1 = new Company(00029947, string.Empty, "Nad stadionem 100, Náchod, 13489, ČR", "Výrobní družstvo invalidů", "Textilní výroba") { Id = 0 };
            var testCompany2 = new Company(27684555, "CZ27684555", "Náměstí T. G. Masaryka 1280, Zlín, 76001, ČR", "Allegro Group CZ", "Reklamní poradenství") { Id = 1 };
            var testCompany3 = new Company(15415422, "CZ1234567899", "Modrá 3, Praha 5, 15000, ČR", "CzechEl", "Výrobce elektro") { Id = 2 };
            
            companyService.SaveCompany(testCompany1);
            companyService.SaveCompany(testCompany2);
            companyService.SaveCompany(testCompany3);

            companyMemoryDao.SaveToFile();
        }
    }
}
