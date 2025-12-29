<%@ Page Title="" Language="C#" MasterPageFile="~/WebsiteMaster.Master" AutoEventWireup="true" CodeBehind="Management.aspx.cs" Inherits="CenturyRayonSchool.Management" %>
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
       
        <div class="head1">Management</div><br />
    </header> 
    <%--<div id="register" class="form-1">--%>
        <div class="container-fluid">
           
                <div class="card prayer-card" style="width: 50rem; margin:auto; border-width:1px;border-style:ridge;border-color:#b2a787bd; border-radius: 25px; padding-left:30px; padding-right:30px">
                     
                    <div class="card-body">
                       
                        <h5 class="card-title" style="text-align: center; color: black; font-family: sans-serif; font-weight: bold; font-size: large"><u>TRUSTEES</u></h5>
                        <br />
                       
                        <table class="table table-hover">
                          <tbody>
                            <tr>
                              <th scope="row">I.</th>
                              <td>Mr. O.R. Chitlange</td>
                              <td></td>
                              <td>Trustee</td>
                            </tr>
                            <tr>
                              <th scope="row">II.</th>
                              <td>Mr. Subodh Dave</td>
                              <td></td>
                              <td>Trustee</td>
                            </tr>
                            <tr>
                              <th scope="row">III.</th>
                              <td>Mr. Yogesh R. Shah</td>
                              <td></td>
                              <td>Trustee</td>
                            </tr>
                          </tbody>
                        </table>


                    </div>

                </div>
            <br />
            <div class="card prayer-card" style="width: 50rem; margin:auto; border-width:1px;border-style:ridge;border-color:#b2a787bd; border-radius: 25px; padding-left:30px; padding-right:30px">
                     
                    <div class="card-body">
                       <h5 class="card-title" style="text-align: center; color: black; font-family: sans-serif; font-weight: bold; font-size: large"><u>SCHOOL MANAGING COMMITTEE</u></h5>
                        <br />
                        <table class="table table-hover">
                          <tbody>
                            <tr>
                              <th scope="row">1.</th>
                              <td>Mr. Yogesh R. Shah </td>
                              <td></td>
                              <td>Chairman</td>
                            </tr>
                            <tr>
                              <th scope="row">2.</th>
                              <td>Mr. Milind Bhandarkar</td>
                              <td></td>
                              <td>Vice-Chairman</td>
                            </tr>
                            <tr>
                              <th scope="row">3.</th>
                              <td>Mr. Anil Sewaney</td>
                              <td></td>
                              <td>Administrator</td>
                            </tr>
                              <tr>
                              <th scope="row">4.</th>
                              <td>Mr. Milind H. Patil</td>
                              <td></td>
                              <td>Treasurer</td>
                            </tr>
                              <tr>
                              <th scope="row">5.</th>
                              <td>Mr. B.P. Karwa</td>
                              <td></td>
                              <td>Member</td>
                            </tr>
                              <tr>
                              <th scope="row">6.</th>
                              <td>Mr. Ajit Patil</td>
                              <td></td>
                              <td>Member</td>
                            </tr>
                               <tr>
                              <th scope="row">7.</th>
                              <td>Mr. Anand N. Thakur</td>
                              <td></td>
                              <td>Member</td>
                            </tr>
                               <tr>
                              <th scope="row">8.</th>
                              <td>Mr. Sameer Saini</td>
                              <td></td>
                              <td>Member</td>
                            </tr>
                               <tr>
                              <th scope="row">9.</th>
                              <td>Mr. Manjeetsingh Kocchar</td>
                              <td></td>
                              <td>Member</td>
                            </tr>
                               <tr>
                              <th scope="row">10.</th>
                              <td>Mr. Leonardo D’Souza</td>
                              <td></td>
                              <td>Member</td>
                            </tr>
                              <tr>
                              <th scope="row">11.</th>
                              <td>Mrs. Ranjna Jhangra</td>
                              <td></td>
                              <td>Member</td>
                            </tr>
                              <tr>
                              <th scope="row">12.</th>
                              <td>Mr. R.B. Singh</td>
                              <td></td>
                              <td>Member</td>
                            </tr>
                              <tr>
                              <th scope="row">13.</th>
                              <td>Mr. Krishna Yadav</td>
                              <td></td>
                              <td>Member</td>
                            </tr>
                              <tr>
                              <th scope="row">14.</th>
                              <td>Mrs. Rachna Mathur</td>
                              <td></td>
                              <td>H.M. (Toodlers)</td>
                            </tr>
                              <tr>
                              <th scope="row">15.</th>
                              <td>Mrs. Ritu Bhagat</td>
                              <td></td>
                              <td>H.M. (Ex-Officio Secy.)</td>
                            </tr>
                               <tr>
                              <th scope="row">16.</th>
                              <td>Mrs. Babita Singh</td>
                              <td></td>
                              <td>H.M. Primary</td>
                            </tr>
                               <tr>
                              <th scope="row">17.</th>
                              <td>Mrs. Sarita Borkar</td>
                              <td></td>
                              <td>Asst. H.M Secondary</td>
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
