// See https://aka.ms/new-console-template for more information
using Figgle;
using Genocs.PGP;

Console.WriteLine(FiggleFonts.Standard.Render("Genocs.PGP!"));

GenerateKeyPair();
EncryptFile();
DecryptFile();
//Console.ReadLine();
//EncryptFile();
//DecryptFile();

//EncryptFileNSign();
//DecryptFileNVerify();
//EncryptFile();
//DecryptNVerify();



/// It generate the keys pairs based on the provided keys 
static void GenerateKeyPair()
{
    using (PGPEncryptDecrypt pgp = new PGPEncryptDecrypt())
        pgp.GenerateKey(@".\data\pub.asc", @".\data\pri.asc", "mark@gmail.com", "Test123");

    Console.WriteLine("PGP KeyPair generated.");
}

static void EncryptFile()
{
    using (PGPEncryptDecrypt pgp = new PGPEncryptDecrypt())
    {
        pgp.EncryptFile(@".\data\SampleData.txt", @".\data\SampleData.PGP", @".\data\pub.asc", true, false);
        Console.WriteLine("PGP Encryption done.");
    }
}

static void DecryptFile()
{
    using (PGPEncryptDecrypt pgp = new PGPEncryptDecrypt())
    {
        pgp.DecryptFile(@".\data\SampleData.PGP", @".\data\SampleData.out.txt", @".\data\pri.asc", "Test123");
        Console.WriteLine("PGP Decryption done.");
    }
}

static void Encrypt()
{
    using (PGPEncryptDecrypt pgp = new PGPEncryptDecrypt())
    {
        using (Stream input = File.OpenRead("SampleData.txt"))
        {
            using (Stream output = File.OpenWrite("SampleData.PGP"))
            {
                //pgp.CompressionAlgorithm = ChoCompressionAlgorithm.Zip;
                pgp.Encrypt(input, output, "Sample_Pub.asc", true, false);
            }
        }
    }
}
static void Decrypt()
{
    using (PGPEncryptDecrypt pgp = new PGPEncryptDecrypt())
    {
        using (Stream input = File.OpenRead("SampleData.PGP"))
        {
            using (Stream output = File.OpenWrite("SampleData.OUT"))
            {
                pgp.Decrypt(input, output, "Sample_Pri.asc", "Test123");
            }
        }
        Console.WriteLine("PGP Decryption done.");
    }
}

static void EncryptFileNSign()
{
    using (PGPEncryptDecrypt pgp = new PGPEncryptDecrypt())
    {
        //pgp.CompressionAlgorithm = ChoCompressionAlgorithm.Zip;
        pgp.EncryptFileAndSign("SampleData.txt", "SampleData.PGP", "Sample_Pub.asc", "Sample_Pri.asc", "Test123", true, false);
        Console.WriteLine("PGP Encryption done.");
    }
}

static void DecryptFileNVerify()
{
    using (PGPEncryptDecrypt pgp = new PGPEncryptDecrypt())
    {
        pgp.DecryptFileAndVerify("SampleData.PGP", "SampleData.OUT", "Sample_Pub.asc", "Sample_Pri.asc", "Test123");
        Console.WriteLine("PGP Decryption done.");
    }
}

static void EncryptNSign()
{
    using (PGPEncryptDecrypt pgp = new PGPEncryptDecrypt())
    {
        using (Stream input = File.OpenRead(@".\SampleData.txt"))
        {
            using (Stream output = File.OpenWrite(@".\SampleData.PGP"))
            {
                //pgp.CompressionAlgorithm = ChoCompressionAlgorithm.Zip;
                pgp.EncryptAndSign(input, output, "Sample_Pub.asc", "Sample_Pri.asc", "Test123", true, false);
            }
        }
    }
    Console.WriteLine("PGP Encryption done.");
}

static void DecryptNVerify()
{
    using PGPEncryptDecrypt pgp = new PGPEncryptDecrypt();
    using (Stream input = File.OpenRead("SampleData.PGP"))
    {
        using Stream output = File.OpenWrite("SampleData.OUT");
        pgp.DecryptAndVerify(input, output, "Sample_Pub.asc", "Sample_Pri.asc", "Test123");
    }
    Console.WriteLine("PGP Decryption done.");
}

