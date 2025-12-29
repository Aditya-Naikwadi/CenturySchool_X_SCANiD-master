<%@ Page Title="" Language="C#" MasterPageFile="~/WebsiteMaster.Master" AutoEventWireup="true" CodeBehind="ChairmanMessage.aspx.cs" Inherits="CenturyRayonSchool.ChairmanMessage" %>
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
        <div class="head1">From the Chairman's desk</div><br />
    </header> 
         
        <div class="container-fluid">
            <div class="card chairmanmsg" style="width: 50rem; margin:auto; border-width:1px;border-style:ridge;border-color:#b2a787bd; border-radius: 25px; padding-left:10px">
              
                <div class="card body">
                    <p class="card-text" style="text-align:match-parent; color:ActiveCaption; font-family:sans-serif; font-size:large; line-height: 28px; margin-left:12px">
                        Education is the process of facilitating learning or the acquisition of knowledge, skills, values, morals and personal development. Quality education refers to the knowledge received through schooling. The main purpose of education is the integral development of a child. It decides a child behaviour, aspirations, Introspections and learning to achieve success. We, at Century Rayon School, follow this philosophy to stimulate thinking skills, improve children's critical, creative thinking, improve communication skills to meet ever-growing challenges being faced in a competitive environment. The values and environment we provide in the school to your children are sustainable and see them successful in their formative years. As the chairman of the school, I assure you that the future of your child is secure and we would continue to put efforts to realise greater success.
                    </p>
                    <p class="card-text" style="text-align:match-parent; color:ActiveCaption; font-family:sans-serif; font-size:large; line-height: 28px; margin-left:12px">
                        With best wishes................... <br>
                    <label style="font-weight: bold;">Chairman</label> 
                    </p>
                </div>
                <div class="nicdark_space10"></div>
            </div>
        </div>
 <div class="nicdark_space10"></div>

         </section>
</asp:Content>
