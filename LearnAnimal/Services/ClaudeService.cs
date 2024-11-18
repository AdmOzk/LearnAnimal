using Amazon.BedrockRuntime;
using Amazon.BedrockRuntime.Model;
using Amazon.Runtime;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LearnAnimal.Services
{
    public class ClaudeService
    {
        private readonly AmazonBedrockRuntimeClient _bedrockClient;

        public ClaudeService(IConfiguration configuration)
        {
            var accessKeyId = configuration["AWS:AccessKeyId"];

            var secretAccessKey = configuration["AWS:SecretAccessKey"];

            var sessionToken = configuration["AWS:SessionToken"];

            var region = configuration["AWS:Region"];

            var credentials = new SessionAWSCredentials(accessKeyId, secretAccessKey, sessionToken);

            var config = new AmazonBedrockRuntimeConfig
            {
                RegionEndpoint = Amazon.RegionEndpoint.GetBySystemName(region)
            };

            _bedrockClient = new AmazonBedrockRuntimeClient(credentials, config);
        }

        public async Task<string> AnimalImage(string base64Image)
        {
            var systemPrompt = $"Fotoğraf: {base64Image}. Bu hayvan nedir? Kısa bilgi ver. (Türkçe)";

            var input = new
            {
                system = systemPrompt,

                messages = new[]
                {
                    new
                    {
                        role = "user",

                         content = new[]
                            {
                                new
                                {
                                    type = "image",
                                    source = new
                                    {
                                        type = "base64",
                                        media_type = "image/png",
                                        data = base64Image
                                    }
                                }
                                },
                    }
                },
                max_tokens = 1000,

                temperature = 0.1,

                top_p = 0.9,

                top_k = 5,

                stop_sequences = new string[] { },

                anthropic_version = "bedrock-2023-05-31"
            }; //bitis

            var inputJson = JsonSerializer.Serialize(input);

            var request = new InvokeModelRequest
            {
                ModelId = "anthropic.claude-3-sonnet-20240229-v1:0",

                ContentType = "application/json",

                Body = new MemoryStream(Encoding.UTF8.GetBytes(inputJson))
            };

            var response = await _bedrockClient.InvokeModelAsync(request);

            using (var reader = new StreamReader(response.Body))
            {
                var responseBody = await reader.ReadToEndAsync();

                var jsonDocument = JsonDocument.Parse(responseBody);

                var root = jsonDocument.RootElement;

                if (root.TryGetProperty("content", out var contentArray) && contentArray.ValueKind == JsonValueKind.Array)
                {
                    foreach (var content in contentArray.EnumerateArray())
                    {
                        if (content.TryGetProperty("text", out var textElement) && textElement.ValueKind == JsonValueKind.String)
                        {
                            return textElement.GetString();
                        }
                    }
                }

                return "Bu hayvan hakkında bilgi bulunamadı.";
            }
        }



        //pdf



        public async Task<string> AnalyzePdfAsync(string base64Pdf)
        {
            var systemPrompt = $"PDF: {base64Pdf}. Bu PDF'deki hayvanlar hakkında bilgi ver. (Türkçe)";

            var input = new
            {
                system = systemPrompt,

                messages = new[]
                {
            new
            {
                role = "user",
                content = new[]
                {
                    new
                    {
                        type = "document",
                        source = new
                        {
                            type = "base64",
                            media_type = "application/pdf",
                            data = base64Pdf
                        }
                    },
                  
                }
            }
        },
                max_tokens = 1500,
                temperature = 0.1,
                top_p = 0.9,
                top_k = 5,
                stop_sequences = new string[] { },
                anthropic_version = "bedrock-2023-05-31"
            };

            var inputJson = JsonSerializer.Serialize(input);

            var request = new InvokeModelRequest
            {
                ModelId = "anthropic.claude-3-sonnet-20240229-v1:0",
                ContentType = "application/json",
                Body = new MemoryStream(Encoding.UTF8.GetBytes(inputJson))
            };

            var response = await _bedrockClient.InvokeModelAsync(request);

            using (var reader = new StreamReader(response.Body))
            {
                var responseBody = await reader.ReadToEndAsync();

                var jsonDocument = JsonDocument.Parse(responseBody);
                var root = jsonDocument.RootElement;

                if (root.TryGetProperty("content", out var contentArray) && contentArray.ValueKind == JsonValueKind.Array)
                {
                    foreach (var content in contentArray.EnumerateArray())
                    {
                        if (content.TryGetProperty("text", out var textElement) && textElement.ValueKind == JsonValueKind.String)
                        {
                            return textElement.GetString();
                        }
                    }
                }

                return "Bu PDF hakkında bilgi bulunamadı.";
            }
        }







        //text


        public async Task<string> GetAnimalInfoAsync(string animalName)
        {
            var systemPrompt = $"Hayvan: {animalName}. Bu hayvan hakkında detaylı bir paragraf bilgi ver.";

            var input = new
            {
                system = systemPrompt,

                messages = new[]
                {
                    new
                    {
                        role = "user",

                        content = $"Hayvan: {animalName} hakkında bilgi ver"
                    }
                },
                max_tokens = 1000,

                temperature = 0.1,

                top_p = 0.9,

                top_k = 5,

                stop_sequences = new string[] { },

                anthropic_version = "bedrock-2023-05-31"
            };

            var inputJson = JsonSerializer.Serialize(input);

            var request = new InvokeModelRequest
            {
                ModelId = "anthropic.claude-3-sonnet-20240229-v1:0",

                ContentType = "application/json",

                Body = new MemoryStream(Encoding.UTF8.GetBytes(inputJson))
            };

            var response = await _bedrockClient.InvokeModelAsync(request);

            using (var reader = new StreamReader(response.Body))
            {
                var responseBody = await reader.ReadToEndAsync();

                var jsonDocument = JsonDocument.Parse(responseBody);

                var root = jsonDocument.RootElement;

                if (root.TryGetProperty("content", out var contentArray) && contentArray.ValueKind == JsonValueKind.Array)
                {
                    foreach (var content in contentArray.EnumerateArray())
                    {
                        if (content.TryGetProperty("text", out var textElement) && textElement.ValueKind == JsonValueKind.String)
                        {
                            return textElement.GetString();
                        }
                    }
                }

                return "Bu hayvan hakkında bilgi bulunamadı.";
            }
        }






    }
}