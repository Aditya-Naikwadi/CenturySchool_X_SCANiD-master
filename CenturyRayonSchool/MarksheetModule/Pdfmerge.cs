using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace CenturyRayonSchool.MarksheetModule
{
    public class Pdfmerge
    {
        public void MergePDFFiles(string[] sourcefiles, string outputPdfPath)
        {
            PdfReader reader = null;
            Document document = null;
            PdfCopy pdfCopyProvider = null;
            PdfImportedPage importedPage = null;

            try
            {

                for (int i = 0; i < sourcefiles.Length; i++)
                {
                    // Intialize a new PdfReader instance with the contents of the source Pdf file:
                    reader = new PdfReader(sourcefiles[i]);

                    // Capture the correct size and orientation for the page:
                    document = new Document(reader.GetPageSizeWithRotation(1));

                    // Initialize an instance of the PdfCopyClass with the source 
                    // document and an output file stream:
                    pdfCopyProvider = new PdfCopy(document,
                        new System.IO.FileStream(outputPdfPath, System.IO.FileMode.Create));

                    document.Open();

                    // Extract the desired page number:
                    importedPage = pdfCopyProvider.GetImportedPage(reader, 1);
                    pdfCopyProvider.AddPage(importedPage);

                    document.Close();
                }


                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }






        }

        public void MergePDF(string destinationFile, string[] sourceFiles)
        {
            List<PdfReader> readerlist = new List<PdfReader>();

            PdfReader reader = null;
            Document document = null;
            PdfWriter writer = null;
            try
            {
                int f = 0;
                // we create a reader for a certain document
                reader = new PdfReader(sourceFiles[f]);
                readerlist.Add(reader);
                // we retrieve the total number of pages
                int n = reader.NumberOfPages;
                //Console.WriteLine("There are " + n + " pages in the original file.");
                // step 1: creation of a document-object
                document = new Document(reader.GetPageSizeWithRotation(1));
                // step 2: we create a writer that listens to the document
                writer = PdfWriter.GetInstance(document, new FileStream(destinationFile, FileMode.Create));
                // step 3: we open the document
                document.Open();
                PdfContentByte cb = writer.DirectContent;
                PdfImportedPage page = null;
                int rotation;
                // step 4: we add content
                while (f < sourceFiles.Length)
                {
                    int i = 0;
                    while (i < n)
                    {
                        i++;
                        document.SetPageSize(reader.GetPageSizeWithRotation(i));
                        document.NewPage();
                        page = writer.GetImportedPage(reader, i);
                        rotation = reader.GetPageRotation(i);
                        if (rotation == 90 || rotation == 270)
                        {
                            cb.AddTemplate(page, 0, -1f, 1f, 0, 0, reader.GetPageSizeWithRotation(i).Height);
                        }
                        else
                        {
                            cb.AddTemplate(page, 1f, 0, 0, 1f, 0, 0);
                        }
                        //Console.WriteLine("Processed page " + i);
                    }
                    f++;
                    if (f < sourceFiles.Length)
                    {
                        // reader.Close();


                        reader = new PdfReader(sourceFiles[f]);
                        readerlist.Add(reader);
                        // we retrieve the total number of pages
                        n = reader.NumberOfPages;
                        //Console.WriteLine("There are " + n + " pages in the original file.");
                    }

                    //page.ClosePath();
                }





                // step 5: we close the document
                writer.Close();
                document.Close();

                //reader.Close();


            }
            catch (Exception e)
            {
                string strOb = e.Message;
                Thread.Sleep(1000);
                //writer.Close();
                //document.Close();

                //reader.Close();

                //writer.Dispose();
                //document.Dispose();
                //reader.Dispose();

            }
            finally
            {
                foreach (PdfReader re in readerlist)
                {
                    re.Close();
                }
            }

        }


        public string[] getAllpdffiles(String folderpath)
        {
            //int filecount = 0;
            //filecount = Directory.GetFiles(folderpath, "*.pdf").Count();
            ////string[] filename = Directory.GetFiles(folderpath, "*.pdf").Select(Path.GetFullPath).ToArray();
            //string[] filename=new string[filecount];

            //for (int i = 0; i < filecount; i++)
            //{
            //    int file=i;

            //    filename[i]=folderpath+"\\"+(file+1).ToString()+".pdf";
            //}

            //return filename;

            string[] filename = Directory.GetFiles(folderpath, "*.pdf")
                                    .Select(Path.GetFullPath)
                                    .ToArray();

            return filename;
        }



        ~Pdfmerge()
        {
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}