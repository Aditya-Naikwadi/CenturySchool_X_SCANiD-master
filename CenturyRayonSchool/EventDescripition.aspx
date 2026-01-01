<%@ Page Title="" Language="C#" MasterPageFile="~/WebsiteMaster.Master" AutoEventWireup="true" CodeFile="EventDescripition.aspx.cs" Inherits="CenturyRayonSchool.EventDescripition" %>
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
        
        <div class="head1">Upcoming Event's</div><br />
    </header> 
       
          <div id="register" class="form-1">
         <div class="container-fluid" >
              <div class="card event-desc">
               
                <div class="nicdark_space20"></div>
                <div class="card-body" >
             <div class="container display-7" style="font-size: 18px; font-family:sans-serif; padding:10px">

                 <div class="">
                     <asp:Label ID="Label1" runat="server" Text="ID:" style="font-weight:bold; color:#ac1616" hidden="hidden"></asp:Label>

                     <asp:Label ID="id1" runat="server" Text="ID" style="color:CaptionText" hidden="hidden"></asp:Label>

                 </div>

                 <div >
                     <asp:Label ID="Label2" runat="server" Text="Event Date:" style="font-weight:bold; color:#ac1616"></asp:Label>

                     <asp:Label ID="Date" runat="server" Text="..." style="color:CaptionText"></asp:Label>
                     <div class="nicdark_space10"></div>
                 </div>

                 <div >
                     <asp:Label ID="Label3" runat="server" Text="Event Name :" style="font-weight:bold; color:#ac1616"></asp:Label>

                     <asp:Label ID="Ename" runat="server" Text="..." style="color:CaptionText"></asp:Label>
                      <div class="nicdark_space10"></div>
                 </div>
                  <div >
                     <asp:Label ID="Label5" runat="server" Text="Event Venue :" style="font-weight:bold; color:#ac1616"></asp:Label>

                     <asp:Label ID="eventvenue" runat="server" Text="..." style="color:CaptionText"></asp:Label>
                      <div class="nicdark_space10"></div>
                 </div>
                 <div >
                     <asp:Label ID="Label6" runat="server" Text="Event Time :" style="font-weight:bold; color:#ac1616"></asp:Label>

                     <asp:Label ID="eventtime" runat="server" Text="..." style="color:CaptionText"></asp:Label>
                      <div class="nicdark_space10"></div>
                 </div>
                 <div >
                     <asp:Label ID="Label4" runat="server" Text="Event Descripition:" style="font-weight:bold; color:#ac1616"></asp:Label>

                     <asp:Label ID="Edesc" runat="server" Text="Event Description" style="color:CaptionText"></asp:Label>
                     <asp:Label ID="filpath" runat="server" Text="filepath" hidden="hidden"></asp:Label>
                      <div class="nicdark_space10"></div>
                 </div>
                 <br />


                 <div class="container  ">
                     <div class="col-sm-offset-2 col-sm-10 ">
                         <% if(isDownloadEnable) 
                                
                            {%>
                         <asp:Button ID="downloadevent" runat="server" Text="Download" OnClick="downloadevent_Click" CssClass="btn btn-success" />
                         <% } %>
                     </div>
                 </div>
             </div>
                    </div>
                  </div>
             <div class="nicdark_space20"></div>
              </div>
         </div>

        </section>
</asp:Content>
