﻿namespace ModernGUI.Shared
{
    public static class Email
    {
        public static class OutlookClient
        {
            public enum BodyType
            {
                PlainText,
                RTF,
                HTML
            }

            /// <summary>
            /// Send an email using outlooks client
            /// here's how you'd use it:
            /*
            List<string> arrAttachFiles = new List<string>() { @"C:\Users\User\Desktop\Picture.png" };

            bool bRes = sendEmailViaOutlook("senders_email@somewhere.com",
                "john.doe@hotmail.com, jane_smith@gmail.com", null,
                "Test email from script - " + DateTime.Now.ToString(),
                "My message body - " + DateTime.Now.ToString(),
                BodyType.PlainText,
                arrAttachFiles,
                null);
            */
            /// </summary>
            /// <param name="sFromAddress"></param>
            /// <param name="sToAddress"></param>
            /// <param name="sCc"></param>
            /// <param name="sSubject"></param>
            /// <param name="sBody"></param>
            /// <param name="bodyType"></param>
            /// <param name="arrAttachments"></param>
            /// <param name="sBcc"></param>
            /// <returns></returns>
            public static bool Send(string sFromAddress, string sToAddress, string sCc, string sSubject, string sBody, BodyType bodyType, List<string> arrAttachments = null, string sBcc = null)
            {
                //Send email via Office Outlook 2010
                //'sFromAddress' = email address sending from (ex: "me@somewhere.com") -- this account must exist in Outlook. Only one email address is allowed!
                //'sToAddress' = email address sending to. Can be multiple. In that case separate with semicolons or commas. (ex: "recipient@gmail.com", or "recipient1@gmail.com; recipient2@gmail.com")
                //'sCc' = email address sending to as Carbon Copy option. Can be multiple. In that case separate with semicolons or commas. (ex: "recipient@gmail.com", or "recipient1@gmail.com; recipient2@gmail.com")
                //'sSubject' = email subject as plain text
                //'sBody' = email body. Type of data depends on 'bodyType'
                //'bodyType' = type of text in 'sBody': plain text, HTML or RTF
                //'arrAttachments' = if not null, must be a list of absolute file paths to attach to the email
                //'sBcc' = single email address to use as a Blind Carbon Copy, or null not to use
                //RETURN:
                //      = true if success
                bool bRes = false;

                try
                {
                    //Get Outlook COM objects
                    Microsoft.Office.Interop.Outlook.Application app = new Microsoft.Office.Interop.Outlook.Application();
                    Microsoft.Office.Interop.Outlook.MailItem newMail = (Microsoft.Office.Interop.Outlook.MailItem)app.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);

                    //Parse 'sToAddress'
                    if (!string.IsNullOrWhiteSpace(sToAddress))
                    {
                        string[] arrAddTos = sToAddress.Split(new char[] { ';', ',' });
                        foreach (string strAddr in arrAddTos)
                        {
                            if (!string.IsNullOrWhiteSpace(strAddr) &&
                                strAddr.IndexOf('@') != -1)
                            {
                                newMail.Recipients.Add(strAddr.Trim());
                            }
                            else
                                throw new Exception("Bad to-address: " + sToAddress);
                        }
                    }
                    else
                        throw new Exception("Must specify to-address");

                    //Parse 'sCc'
                    if (!string.IsNullOrWhiteSpace(sCc))
                    {
                        string[] arrAddTos = sCc.Split(new char[] { ';', ',' });
                        foreach (string strAddr in arrAddTos)
                        {
                            if (!string.IsNullOrWhiteSpace(strAddr) &&
                                strAddr.IndexOf('@') != -1)
                            {
                                newMail.Recipients.Add(strAddr.Trim());
                            }
                            else
                                throw new Exception("Bad CC-address: " + sCc);
                        }
                    }

                    //Is BCC empty?
                    if (!string.IsNullOrWhiteSpace(sBcc))
                    {
                        newMail.BCC = sBcc.Trim();
                    }

                    //Resolve all recepients
                    if (!newMail.Recipients.ResolveAll())
                    {
                        throw new Exception("Failed to resolve all recipients: " + sToAddress + ";" + sCc);
                    }


                    //Set type of message
                    switch (bodyType)
                    {
                        case BodyType.HTML:
                            newMail.HTMLBody = sBody;
                            break;
                        case BodyType.RTF:
                            newMail.RTFBody = sBody;
                            break;
                        case BodyType.PlainText:
                            newMail.Body = sBody;
                            break;
                        default:
                            throw new Exception("Bad email body type: " + bodyType);
                    }


                    if (arrAttachments != null)
                    {
                        //Add attachments
                        foreach (string strPath in arrAttachments)
                        {
                            if (File.Exists(strPath))
                            {
                                newMail.Attachments.Add(strPath);
                            }
                            else
                                throw new Exception("Attachment file is not found: \"" + strPath + "\"");
                        }
                    }

                    //Add subject
                    if (!string.IsNullOrWhiteSpace(sSubject))
                        newMail.Subject = sSubject;

                    Microsoft.Office.Interop.Outlook.Accounts accounts = app.Session.Accounts;
                    Microsoft.Office.Interop.Outlook.Account acc = null;

                    //Look for our account in the Outlook
                    foreach (Microsoft.Office.Interop.Outlook.Account account in accounts)
                    {
                        if (account.SmtpAddress.Equals(sFromAddress, StringComparison.CurrentCultureIgnoreCase))
                        {
                            //Use it
                            acc = account;
                            break;
                        }
                    }

                    //Did we get the account
                    if (acc != null)
                    {
                        //Use this account to send the e-mail. 
                        newMail.SendUsingAccount = acc;

                        //And send it
                        ((Microsoft.Office.Interop.Outlook._MailItem)newMail).Send();

                        //Done
                        bRes = true;
                    }
                    else
                    {
                        throw new Exception("Account does not exist in Outlook: " + sFromAddress);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR: Failed to send mail: " + ex.Message);
                }

                return bRes;
            }
        }
    }
}
