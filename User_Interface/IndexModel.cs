using _2day.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace User_Interface
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public IEnumerable<Student> Students { get; set; }

        public IndexModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task OnGetAsync()
        {
            var client = _clientFactory.CreateClient();
            // URL API может быть абсолютным или относительным, если оба проекта развернуты на одном сервере.
            var response = await client.GetAsync("https://api.myproject.com/api/students");
            if (response.IsSuccessStatusCode)
            {
                Students = await response.Content.ReadFromJsonAsync<IEnumerable<Student>>();
            }
            else
            {
                Students = Enumerable.Empty<Student>();
            }
        }
    }
}
