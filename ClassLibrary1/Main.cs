using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using InjectionApi.Sdk.Client;
using InjectionApi.Sdk.Email;
using RestSharp;

namespace EIPP.SocketSendEmailKarthi
{
    public class EmailMain
    {
        const string apiKey = "d8M7GwDk5m4PNn39Bst6";
        const int serverId = 3529;
        const string apiUrl = "https://inject.socketlabs.com/api/v1/email";

        RestClient client = new RestClient(apiUrl);
        public class PostBodyK
        {
            public string ApiKey = "d8M7GwDk5m4PNn39Bst6";
            public EmailMessage[] Messages;
            public int ServerId = 3529;


            public int sendcustomernattachmentemail(List<string> tolist, List<string> cclist, List<string> bcclist, string From, string Subject, string TextBody, string HtmlBody, List<string> attachmentlist, string MailingId, string MessageId)
            {
                
                PostBodyK postBody = new PostBodyK { Messages = new EmailMessage[1]};
                
                postBody.Messages[0] = new EmailMessage();
                
                postBody.Messages[0].Subject = Subject;
                
                postBody.Messages[0].From = new Address();
                string[] fromlist = From.Split(';');
                if (emailvalidate(fromlist[0]))
                {

                    if (fromlist.Length == 1)
                    {
                        postBody.Messages[0].From.EmailAddress = fromlist[0];
                        postBody.Messages[0].From.FriendlyName = "";
                    }
                    else
                    {
                        postBody.Messages[0].From.EmailAddress = fromlist[0];
                        postBody.Messages[0].From.FriendlyName = fromlist[1];
                    }
                }
                else
                  return 11;
                
                postBody.Messages[0].To = new Address[tolist.Count];
                foreach (string twolistto in tolist)
                {
                    string[] emailname = twolistto.Split(';');
                    if (emailvalidate(emailname[0]))
                    {
                        postBody.Messages[0].To[tolist.IndexOf(twolistto)] = new Address();
                        postBody.Messages[0].To[tolist.IndexOf(twolistto)].EmailAddress = emailname[0];
                        postBody.Messages[0].To[tolist.IndexOf(twolistto)].FriendlyName = emailname[1];
                    }
                    else
                       return 12;
                    
                }

                postBody.Messages[0].Cc = new Address[cclist.Count];
                foreach (string twolistcc in cclist)
                {
                    string[] emailname = twolistcc.Split(';');
                    if (emailvalidate(emailname[0]))
                    {
                        postBody.Messages[0].Cc[cclist.IndexOf(twolistcc)] = new Address();
                        postBody.Messages[0].Cc[cclist.IndexOf(twolistcc)].EmailAddress = emailname[0];
                        postBody.Messages[0].Cc[cclist.IndexOf(twolistcc)].FriendlyName = emailname[1];
                    }
                    else
                        return 13;
                }

                postBody.Messages[0].Bcc = new Address[bcclist.Count];
                foreach (string twolistbcc in bcclist)
                {
                    string[] emailname = twolistbcc.Split(';');
                    if (emailvalidate(emailname[0]))
                    {
                        postBody.Messages[0].Bcc[bcclist.IndexOf(twolistbcc)] = new Address();
                        postBody.Messages[0].Bcc[bcclist.IndexOf(twolistbcc)].EmailAddress = emailname[0];
                        postBody.Messages[0].Bcc[bcclist.IndexOf(twolistbcc)].FriendlyName = emailname[1];
                    }
                    else
                    {
                        return 14;
                    }
                }
                
                postBody.Messages[0].Attachments = new Attachment[attachmentlist.Count];
                foreach (string twolistatt in attachmentlist)
                {
                    string[] emailname = twolistatt.Split(';');
                    postBody.Messages[0].Attachments[attachmentlist.IndexOf(twolistatt)] = new Attachment();
                    postBody.Messages[0].Attachments[attachmentlist.IndexOf(twolistatt)].Name = emailname[0];
                    postBody.Messages[0].Attachments[attachmentlist.IndexOf(twolistatt)].ContentType = emailname[1];
                    byte[] pdfBytes = File.ReadAllBytes(emailname[2]);
                    postBody.Messages[0].Attachments[attachmentlist.IndexOf(twolistatt)].Content = Convert.ToBase64String(pdfBytes);

                }

                postBody.Messages[0].TextBody = TextBody;
                postBody.Messages[0].HtmlBody = HtmlBody;


                postBody.Messages[0].MailingId = MailingId;
                postBody.Messages[0].MessageId = MessageId;
          try
            {
                // Generate a new POST request.
                var request = new RestRequest(Method.POST) { RequestFormat = DataFormat.Json };

                // Store the request data in the request object.
                request.AddBody(postBody);

                // Make the POST request.
                RestClient client = new RestClient("https://inject.socketlabs.com/api/v1/email");
                var result = client.ExecuteAsPost(request, "POST");

                // Store the response result in our custom class.
                using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(result.Content)))
                {
                    var serializer = new DataContractJsonSerializer(typeof(PostResponse));
                    var resp = (PostResponse)serializer.ReadObject(stream);

                    // Display the results.
                    if (resp.ErrorCode.Equals("Success"))
                    {
                        return 0;
                    }
                    else
                    {
                        return 1;
                       // Console.WriteLine(result.Content);
                    }
                }
            }
            catch (Exception ex)
            {
                return 2;
            }
                
            
            }

            public int sendproductionemail(List<string> tolist, List<string> cclist, List<string> bcclist, string From, string Subject, string TextBody, string HtmlBody, List<string> attachmentlist, string MailingId, string MessageId)
            {
                PostBodyK postBody = new PostBodyK { Messages = new EmailMessage[1] };

                postBody.Messages[0] = new EmailMessage();

                postBody.Messages[0].Subject = Subject;

                postBody.Messages[0].From = new Address();
                string[] fromlist = From.Split(';');
                if (emailvalidate(fromlist[0]))
                {
                    postBody.Messages[0].From.EmailAddress = fromlist[0];
                    postBody.Messages[0].From.FriendlyName = fromlist[1];
                }
                else
                {
                    return 11;
                }
                postBody.Messages[0].To = new Address[tolist.Count];
                foreach (string twolistto in tolist)
                {
                    string[] emailname = twolistto.Split(';');
                    
                    postBody.Messages[0].To[tolist.IndexOf(twolistto)] = new Address();
                    postBody.Messages[0].To[tolist.IndexOf(twolistto)].EmailAddress = emailname[0];
                    postBody.Messages[0].To[tolist.IndexOf(twolistto)].FriendlyName = emailname[1];

                }

                postBody.Messages[0].Cc = new Address[cclist.Count];
                foreach (string twolistcc in cclist)
                {
                    string[] emailname = twolistcc.Split(';');
                    postBody.Messages[0].Cc[cclist.IndexOf(twolistcc)] = new Address();
                    postBody.Messages[0].Cc[cclist.IndexOf(twolistcc)].EmailAddress = emailname[0];
                    postBody.Messages[0].Cc[cclist.IndexOf(twolistcc)].FriendlyName = emailname[1];

                }

                postBody.Messages[0].Bcc = new Address[bcclist.Count];
                foreach (string twolistbcc in bcclist)
                {
                    string[] emailname = twolistbcc.Split(';');
                    postBody.Messages[0].Bcc[bcclist.IndexOf(twolistbcc)] = new Address();
                    postBody.Messages[0].Bcc[bcclist.IndexOf(twolistbcc)].EmailAddress = emailname[0];
                    postBody.Messages[0].Bcc[bcclist.IndexOf(twolistbcc)].FriendlyName = emailname[1];

                }

                postBody.Messages[0].Attachments = new Attachment[attachmentlist.Count];
                foreach (string twolistatt in attachmentlist)
                {
                    string[] emailname = twolistatt.Split(';');
                    postBody.Messages[0].Attachments[attachmentlist.IndexOf(twolistatt)] = new Attachment();
                    postBody.Messages[0].Attachments[attachmentlist.IndexOf(twolistatt)].Name = emailname[0];
                    postBody.Messages[0].Attachments[attachmentlist.IndexOf(twolistatt)].ContentType = emailname[1];
                    byte[] pdfBytes = File.ReadAllBytes(emailname[2]);
                    postBody.Messages[0].Attachments[attachmentlist.IndexOf(twolistatt)].Content = Convert.ToBase64String(pdfBytes);

                }

                postBody.Messages[0].TextBody = TextBody;
                postBody.Messages[0].HtmlBody = HtmlBody;


                postBody.Messages[0].MailingId = MailingId;
                postBody.Messages[0].MessageId = MessageId;
                try
                {
                    // Generate a new POST request.
                    var request = new RestRequest(Method.POST) { RequestFormat = DataFormat.Json };

                    // Store the request data in the request object.
                    request.AddBody(postBody);

                    // Make the POST request.
                    RestClient client = new RestClient("https://inject.socketlabs.com/api/v1/email");
                    var result = client.ExecuteAsPost(request, "POST");

                    // Store the response result in our custom class.
                    using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(result.Content)))
                    {
                        var serializer = new DataContractJsonSerializer(typeof(PostResponse));
                        var resp = (PostResponse)serializer.ReadObject(stream);

                        // Display the results.
                        if (resp.ErrorCode.Equals("Success"))
                        {
                            return 0;
                        }
                        else
                        {
                            return 1;
                            // Console.WriteLine(result.Content);
                        }
                    }
                }
                catch (Exception ex)
                {
                    return 2;
                }


            }
            public bool emailvalidate(string email)
            {
                bool isEmail = Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
                if (isEmail)
                    return true;
                else
                    return false;

            }
            
        }
    }
}
