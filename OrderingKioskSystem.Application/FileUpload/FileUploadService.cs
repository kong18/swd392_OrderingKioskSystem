using Firebase.Storage;
using Google.Apis.Auth.OAuth2;
using OrderingKioskSystemManagement.Application;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;


namespace OrderingKioskSystem.Application.FileUpload
{
    public class FileUploadService
    {
        private readonly FirebaseConfig _firebaseConfig;

        public FileUploadService(FirebaseConfig firebaseConfig)
        {
            _firebaseConfig = firebaseConfig;
        }

        public async Task<string> UploadFileAsync(Stream fileStream, string fileName)
        {
            try
            {
                var resizedStream = new MemoryStream();
                CompressImage(fileStream, resizedStream);

                var jwtToken = await GenerateJwtTokenAsync();

                var firebaseStorage = new FirebaseStorage(
                    _firebaseConfig.StorageBucket,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(jwtToken)
                    });

                var task = firebaseStorage
                    .Child("uploads") // Ensure the path exists
                    .Child(fileName)
                    .PutAsync(resizedStream);

                var downloadUrl = await task;

                return downloadUrl;
            }
            catch (Exception ex)
            {
                // Log exception details
                Console.WriteLine($"Error uploading file: {ex.Message}");
                throw;
            }
        }

        private void CompressImage(Stream inputStream, Stream outputStream, int quality = 75)
        {
            using (var image = Image.Load(inputStream))
            {
                var encoder = new JpegEncoder
                {
                    Quality = quality // Adjust the quality (1-100) to compress the image
                };

                image.Save(outputStream, encoder);
                outputStream.Seek(0, SeekOrigin.Begin);
            }
        }

        private async Task<string> GenerateJwtTokenAsync()
        {
            var jsonCredentials = $@"
            {{
                ""type"": ""{_firebaseConfig.Type}"",
                ""project_id"": ""{_firebaseConfig.ProjectId}"",
                ""private_key_id"": ""{_firebaseConfig.PrivateKeyId}"",
                ""private_key"": ""{_firebaseConfig.PrivateKey.Replace("\\n", "\n")}"",
                ""client_email"": ""{_firebaseConfig.ClientEmail}"",
                ""client_id"": ""{_firebaseConfig.ClientId}"",
                ""auth_uri"": ""{_firebaseConfig.AuthUri}"",
                ""token_uri"": ""{_firebaseConfig.TokenUri}"",
                ""auth_provider_x509_cert_url"": ""{_firebaseConfig.AuthProviderX509CertUrl}"",
                ""client_x509_cert_url"": ""{_firebaseConfig.ClientX509CertUrl}""
            }}";

            var credential = GoogleCredential.FromJson(jsonCredentials).CreateScoped(new[] { "https://www.googleapis.com/auth/firebase" });

            var token = await credential.UnderlyingCredential.GetAccessTokenForRequestAsync();

            return token;
        }
    }
}
