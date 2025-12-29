<%@ Page Title="" Language="C#" MasterPageFile="~/WebsiteMaster.Master" AutoEventWireup="true" CodeBehind="Officehours.aspx.cs" Inherits="CenturyRayonSchool.Officehours" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://fonts.googleapis.com/css?family=Montserrat:400,400i,600,700,700i&display=swap" rel="stylesheet" />
    <link href='https://fonts.googleapis.com/css2?family=Quintessential&display=swap' rel='stylesheet'/>


    
    <link href="../css/fontawesome-all.css" rel="stylesheet"/>

    
    <style>
        .head1{
            font-size:xxx-large;
    font-weight: 600;
    margin-top: 115px;
    color:#74cee4;
    font-family:Quintessential;
    text-align:center;

        }

        h1 {
    color: #333;
    font: 700 2.75rem/3.375rem "Montserrat", sans-serif;
}
        h3 {
    color: #333;
    font: 700 1.625rem/2.125rem "Montserrat", sans-serif;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <section class="nicdark_section" style="background-color:#9d95951a">
       <header id="header" class="header" >
       
        <div class="head1">Office Hours</div><br />
    </header> 
    <%--<div id="register" class="form-1">--%>
        <div class="container-fluid">
           
                <div class="card prayer-card" style="width: 50rem; margin:auto; border-width:1px;border-style:ridge;border-color:#b2a787bd; border-radius: 25px; padding-left:30px; padding-right:30px">
                     
                    <div class="card-body">
                       
                       
                        <table class="table table-hover">
                          <tbody>
                            <tr>
                              <th scope="row">1.</th>
                              <td>Office Hours</td>
                              <td>:</td>
                              <td>10.30 a.m. to 5.30 p.m.</td>
                            </tr>
                            <tr>
                              <th scope="row">2.</th>
                              <td>Morning Shift</td>
                              <td>:</td>
                              <td>7.00 a.m. to 12.35 p.m.</td>
                            </tr>
                            <tr>
                              <th scope="row">3.</th>
                              <td>Afternoon Shift</td>
                              <td>:</td>
                              <td>12.30 p.m. to 6.05 p.m.</td>
                            </tr>
                          </tbody>
                        </table>


                    </div>

                </div>
                
        </div>
    <%--</div>--%>
    <div class="nicdark_space20"></div>
 </section>




</asp:Content>
