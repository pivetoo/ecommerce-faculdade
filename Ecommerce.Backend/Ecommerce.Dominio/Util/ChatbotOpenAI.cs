using Ecommerce.Dominio.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;

namespace Ecommerce.Dominio.Util
{
    public class ChatbotOpenAI
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly ChatbotContentService _chatBotContentService;

        public ChatbotOpenAI(IConfiguration configuration, ChatbotContentService chatBotContentService)
        {
            _httpClient = new HttpClient();
            _apiKey = configuration["OpenAI:ApiKey"];
            _chatBotContentService = chatBotContentService;
        }

        public async Task<string> ObterRespostaAsync(string pergunta)
        {
            var systemMessages = (await _chatBotContentService.ListarConteudos())
                .Where(c => c.Role == "system")
                .Select(c => new { role = c.Role, content = c.Content })
                .ToList();

            var messagesChat = systemMessages.Append(new { role = "user", content = pergunta }).ToList();

            var requestBody = new
            {
                model = "gpt-3.5-turbo",
                messages = messagesChat,
                max_tokens = 150
            };

            var requestContent = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
            var response = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", requestContent);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Erro na requisição: {responseContent}");
            }

            var gptResponse = JsonConvert.DeserializeObject<GptApiResponse>(responseContent);
            return gptResponse.Choices[0].Message.Content.Trim();
        }

        public class GptApiResponse
        {
            public GptChoice[] Choices { get; set; }
        }

        public class GptChoice
        {
            public GptMessage Message { get; set; }
        }

        public class GptMessage
        {
            public string Content { get; set; }
        }
    }
}
