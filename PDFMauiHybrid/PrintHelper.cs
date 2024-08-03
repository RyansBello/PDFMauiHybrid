using Microsoft.JSInterop;
using Xamarin.Essentials;
using FileSystem = Xamarin.Essentials.FileSystem;
using Launcher = Xamarin.Essentials.Launcher;
using OpenFileRequest = Xamarin.Essentials.OpenFileRequest;
using ReadOnlyFile = Xamarin.Essentials.ReadOnlyFile;


namespace PDFMauiHybrid
{
    public class PrintHelper
    {
        [JSInvokable]
        public static async Task ReceivePdf(string base64Data, string filename)
        {
            // Potong header dari data Base64 jika ada
            var base64 = base64Data.Split(',')[1];
            var data = Convert.FromBase64String(base64);

            // Tulis ke file atau lakukan tindakan lainnya
            var filePath = Path.Combine(FileSystem.AppDataDirectory, filename);
            await File.WriteAllBytesAsync(filePath, data);
            Console.WriteLine($"PDF saved to {filePath}");

            var printHelper = new PrintHelper();
            await printHelper.OpenPdfFileAsync(filePath);
        }

        public async Task OpenPdfFileAsync(string filePath)
        {
            await Launcher.OpenAsync(new OpenFileRequest
            {
                File = new ReadOnlyFile(filePath),
                Title = "Open PDF"
            });
        }
    }
}
