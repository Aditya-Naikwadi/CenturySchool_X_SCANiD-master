<%@ Page Title="" Language="C#" MasterPageFile="~/WebsiteMaster.Master" AutoEventWireup="true" CodeBehind="PrincipalMessage.aspx.cs" Inherits="CenturyRayonSchool.PrincipalMessage" %>
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
        <div class="head1">From the H.M's Desk</div><br />
    </header> 
        <div class="container-fluid">
            <div class="card principalmsg" style="width: 50rem; margin:auto; border-width:1px;border-style:ridge;border-color:#b2a787bd; border-radius: 25px;  padding-left:30px; padding-right:30px">
              
                <div class="card body">
                    <p class="card-text" style="text-align:match-parent; color:ActiveCaption; font-family:sans-serif; font-size:large; line-height: 28px; margin-left:12px">
                       I take massive pleasure to welcome you to the website of Century Rayon school. The school website is most collaborative for parents and students. It furnishes a lot of necessary details and facts. We, at Century Rayon School, think that every child is specific and require an open schooling atmosphere. Where a child is allowed to believe, reflects to ask doubts without hesitation. Here learning is acquired knowledge through study or by experiences. If a child cannot learn the way we teach, we must teach in a way the child can learn. We get ready your children by furnishing them with more chances to learn problem-solving skills with compassion. As parents, you want the best for your child. We ensure you that they are given the right knowledge to face future challenges. Education is a shared commitment between dedicated teachers, motivated students and enthusiastic parents with high expectations. We thank you for taking the time to visit the school website. Contact us in case you like to know more about our school. We are bound to keep on improving our devices to provide quality education to your children.
                    </p>
                    <p class="card-text" style="text-align:match-parent; color:ActiveCaption; font-family:sans-serif; font-size:large; line-height: 28px; margin-left:12px">
                        Kind Regards................... <br>
                    <label style="font-weight: bold;">H.M.....</label> 
                    </p>
                </div>
                <div class="nicdark_space10"></div>
            </div>
        </div>
 <div class="nicdark_space10"></div>
        </section>
</asp:Content>
