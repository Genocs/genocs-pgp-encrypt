using Org.BouncyCastle.Bcpg.OpenPgp;

namespace Genocs.PGP;

public static class PGPUtility
{
    public static void WriteStreamToLiteralData(Stream output, char fileType, Stream input, string name)
    {
        PgpLiteralDataGenerator lData = new PgpLiteralDataGenerator();
        Stream pOut = lData.Open(output, fileType, name, input.Length, DateTime.Now);
        PipeStreamContents(input, pOut, 4096);
    }

    public static void WriteStreamToLiteralData(
        Stream output,
        char fileType,
        Stream input,
        byte[] buffer,
        string name)
    {
        PgpLiteralDataGenerator lData = new PgpLiteralDataGenerator();
        Stream pOut = lData.Open(output, fileType, name, DateTime.Now, buffer);
        PipeStreamContents(input, pOut, buffer.Length);
    }

    private static void PipeFileContents(FileInfo file, Stream pOut, int bufSize)
    {
        using (FileStream inputStream = file.OpenRead())
        {
            byte[] buf = new byte[bufSize];

            int len;
            while ((len = inputStream.Read(buf, 0, buf.Length)) > 0)
            {
                pOut.Write(buf, 0, len);
            }
        }
    }

    private static void PipeStreamContents(Stream input, Stream pOut, int bufSize)
    {
        byte[] buf = new byte[bufSize];

        int len;
        while ((len = input.Read(buf, 0, buf.Length)) > 0)
        {
            pOut.Write(buf, 0, len);
        }
    }
}
