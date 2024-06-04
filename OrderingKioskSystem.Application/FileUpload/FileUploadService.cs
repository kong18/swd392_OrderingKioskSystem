using Firebase.Storage;
using OrderingKioskSystemManagement.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var firebaseStorage = new FirebaseStorage(
                _firebaseConfig.ProjectId + ".appspot.com",
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(_firebaseConfig.PrivateKey)
                });

            var task = firebaseStorage
                .Child("uploads")
                .Child(fileName)
                .PutAsync(fileStream);

            var downloadUrl = await task;

            return downloadUrl;
        }
    }
}
