<%@ Page Title="" Language="C#" MasterPageFile="~/WebsiteMaster.Master" AutoEventWireup="true" CodeBehind="Visitinghours.aspx.cs" Inherits="CenturyRayonSchool.Visitinghours" %>
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
       
        <div class="head1">Visiting Hours</div><br />
    </header> 
    <%--<div id="register" class="form-1">--%>
        <div class="container-fluid">
           
                <div class="card prayer-card" style="width: 50rem; margin:auto; border-width:1px;border-style:ridge;border-color:#b2a787bd; border-radius: 25px; padding-left:30px; padding-right:30px">
                    <div class="card-body">
                        <h5 class="card-title" style="text-align: center; color: black; font-family: sans-serif; font-weight: bold; font-size: large"><u>FOR PARENTS</u></h5>
                        <br />
                        <table class="table table-hover table-borderless">
                          <tbody>
                            <tr>
                              <td>Office Hours: 10.30 a.m. to 1.00 p.m. only.</td>
                            </tr>
                            <tr>
                              <td>Fees : Fees to be deposited in UCO Bank, as per the date given by school authority.</td>
                            </tr>
                               <tr>
                              <td>H.M.	: H.M. will be available between 11.00 a.m. to 12.30 p.m. only on Saturday.</td>
                              </tr>
                               <tr>
                              <td>Supervisors & Teachers : 20 Minutes after school hours.</td>
                            </tr>
                          </tbody>
                        </table>
                        <hr />
                         <table class="table table-hover table-borderless">
                          <tbody>
                            <tr>
                              <td colspan="3">Parents should contact the class teachers regarding their wards on the last date of every month between the following timings.</td>
                            </tr>
                            <tr>
                              
                              <td>English Medium</td>
                              <td>:</td>
                              <td>10.00 a.m. to 11.00 a.m.</td>
                            </tr>

                              <tr>
                              
                              <td>Hindi/Marathi Medium</td>
                              <td>:</td>
                              <td>04.00 p.m. to 05.00 p.m.</td>
                            </tr>
                           
                          </tbody>
                        </table>
                        <hr />
                        <table class="table table-hover table-borderless">
                          <tbody>
                            <tr>
                              <td>It is compulsory for parents to see the answer paper of their wards on open day.</td>
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
