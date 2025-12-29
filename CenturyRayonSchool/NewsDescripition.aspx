<%@ Page Title="" Language="C#" MasterPageFile="~/WebsiteMaster.Master" AutoEventWireup="true" CodeBehind="NewsDescripition.aspx.cs" Inherits="CenturyRayonSchool.NewsDescripition" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href='https://fonts.googleapis.com/css2?family=Quintessential&display=swap' rel='stylesheet'/>
    <style>
        .head1{
            font-size:xxx-large;
    font-weight: 600;
    margin-top: 115px;
    color:#74cee4;
    font-family:Quintessential;
    text-align:center;

        }

        .btn {
    display: inline-block;
    font-weight: 400;
    color: #212529;
    text-align: center;
    vertical-align: middle;
    -webkit-user-select: none;
    -moz-user-select: none;
    -ms-user-select: none;
    user-select: none;
    background-color: transparent;
    border: 1px solid transparent;
    padding: 0.375rem 0.75rem;
    font-size: 1rem;
    line-height: 1.5;
    border-radius: 0.25rem;
    transition: color .15s ease-in-out,background-color .15s ease-in-out,border-color .15s ease-in-out,box-shadow .15s ease-in-out;
}

        .btn:not(:disabled):not(.disabled) {
    cursor: pointer;
}

        .btn-success {
    color: #fff;
    background-color: #28a745;
    border-color: #28a745;
}


    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="nicdark_section" style="background-color:#9d95951a">
    <header id="header" class="header" >
        <!--  <div class="container-fluid">
            <div class="row">
                <div class="col-lg-12">
                   
                </div> 
            </div> 
        </div>  -->
        <div class="head1">Notice</div><br />
    </header> 
       
          <div id="register" class="form-1">
         <div class="container-fluid" >
              <div class="card" style="width: 50rem; margin-left: 350px; border-width:1px;border-style:ridge;border-color:#b2a787bd; border-radius: 25px; padding-left:30px; padding-right:30px">
               
                <div class="nicdark_space20"></div>
                <div class="card-body" >

             <div  class="container display-7" style="font-size: 18px; font-family:sans-serif; padding:10px">
                 <div>
                     <asp:Label ID="Label1" runat="server" Text="ID: " style="font-weight:bold; color:#ac1616" hidden="hidden"></asp:Label>

                     <asp:Label ID="id1" runat="server" Text="ID" style="color:CaptionText" hidden="hidden"></asp:Label>

                 </div>

                 <div>
                     <asp:Label ID="Label2" runat="server" Text="Date: " style="font-weight:bold; color:#ac1616" hidden="hidden"></asp:Label>

                     <asp:Label ID="Date" runat="server" Text="DATE" style="color:CaptionText" hidden="hidden"></asp:Label>

                 </div>

                 <div>
                     <asp:Label ID="Label3" runat="server" Text="Notice Name: " style="font-weight:bold; color:#ac1616"></asp:Label>

                     <asp:Label ID="Tname" runat="server" Text="Topic Name" style="color:CaptionText"></asp:Label>
                 </div>
                  <div class="nicdark_space10"></div>

                 <div >
                     <asp:Label ID="Label4" runat="server" Text="Notice Description: " style="font-weight:bold; color:#ac1616"></asp:Label>

                     <asp:Label ID="Tdesc" runat="server" Text="Notice Description" style="color:CaptionText"></asp:Label>
                     <asp:Label ID="filpath" runat="server" Text="filepath" hidden="hidden"></asp:Label>
                 </div><br />
                    <div class="nicdark_space10"></div>


                 <div class="container">
                     <div class="col-sm-offset-2 col-sm-10 success">
                         <% if(isDownloadEnable) 
                                
                            {%>
                        
                         <asp:Button ID="download" runat="server" Text="Download" OnClick="download_Click" CssClass="btn btn-success"/>
                        
                         <% } %>
                     </div>
                 </div>
                   <div class="nicdark_space10"></div>
             </div>
                    </div>
                  </div>
              <div class="nicdark_space20"></div>
              </div>
         </div>
         

        </section>
</asp:Content>
