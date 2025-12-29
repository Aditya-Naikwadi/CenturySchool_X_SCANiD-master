<%@ Page Title="" Language="C#" MasterPageFile="~/WebsiteMaster.Master" AutoEventWireup="true" CodeBehind="MissionVision.aspx.cs" Inherits="CenturyRayonSchool.MissionVision" %>
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
        <div class="head1">Mission & Vission</div><br />
    </header> 

        <div class="container-fluid">
           <div class="card missionV" style="width: 50rem; margin:auto; border-width:1px;border-style:ridge;border-color:#b2a787bd; border-radius: 25px; padding-left:30px; padding-right:30px">
                
               <div class="nicdark_space20"></div>
                <div class="card-body" >
                    <p class="card-text" style="text-align:match-parent; color:ActiveCaption; font-family:sans-serif; font-size:large; line-height: 28px; margin-left:12px">
                        We at Century Rayon High School Shahad strive to provide our students the best opportunities for enhancing their inherent and acquired potentials, instilling in them a belief in life-long learning and thereby motivating them to be responsible citizens and productive participants in the growth of family, society and country.
                    </p><br />
                    <p class="card-text" style="text-align:match-parent; color:ActiveCaption; font-family:sans-serif; font-size:large; line-height: 28px; margin-left:12px">
                        We are committed to continually improve in terms of technology, curriculum, human resources and infrastructure to meet the future advancements in education.
                    </p>
                </div>
               
        <div class="nicdark_space20"></div>
            
           <div class="card">
            
               <div  class="card-body">
                  <div class="card-title" style="font-size:large; font-family:sans-serif; font-weight:bold">Core Competencies</div>  

                  <div style="margin-top: 15px;color:ActiveCaption; font-family:sans-serif; line-height: 28px;" class="para1 coreC">
                    <ul style="list-style-type:square">
                        <li>Working together towards a common goal of taking the school to greater heights.</li>
                        <li>Regular faculty development through trainings and workshops.</li>
                        <li>Adopting new teaching learning methods.</li>
                        <li>Innovations for excellent results.</li>
                        <li>Blend of scholastic & co-scholastic activities for the holistic development of the students.</li>
                      </ul>
                  </div>
               </div>
               <div class="nicdark_space40"></div>

           </div>
        </div>
     </div>
        </section>
</asp:Content>
