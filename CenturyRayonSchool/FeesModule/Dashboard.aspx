<%@ Page Title="" Language="C#" MasterPageFile="~/FeesModule/Master.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="CenturyRayonSchool.FeesModule.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .font-color{
            color:navy !important;
        }

        .uppercase{
            text-transform:uppercase;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-header">
        <div class="row">
            <div class="col-sm-10">
                <h3 class="page-title fs-16">Welcome, &nbsp <asp:Label runat="server" Text="Insiya Kanchwala" ID="lbldisplayusername" CssClass="uppercase"></asp:Label></h3>
               
            </div>
            <div class="col-sm-2">
                <asp:Label Text="Academic Year :" runat="server" />
                 <h3 class="page-title fs-16" >
                     <asp:Label runat="server" ID="lblAcademicyear" ></asp:Label>
                      
                 </h3>
            </div>
        </div>
        <div class="row">
             <div class="col-sm-12">
                <ul class="breadcrumb">
                    <li class="breadcrumb-item active">Dashboard</li>
                    <button runat="server" id="goto" class="btn btn-primary btn-lg btn-block" onserverclick="goto_ServerClick" style="width: auto; margin-left: auto;">Go To Selection Module</button>
                </ul>
            </div>
        </div>
    </div>
    <section class="content mb-1">
        <div class="container-fluid">
            <div class="row">

                <% if (checkStatus("feeheader",listmodules))
                    { %> 
                        <div class="col-md-4">
                            <a href="FeesHeader.aspx">
                                <div class="card dash-widget border-card">
                                    <div class="card-body">
                                        <span class="dash-widget-icon"><i class="fas fa-list"></i></span>
                                        <div class="div-header">
                                            <span class="span-header font-color">Fee Headers</span>
                                        </div>
                                    </div>
                                </div>
                            </a>
                        </div>
                <% } %> 

                  <% if (checkStatus("feeparticular",listmodules))
                    { %>
                        <div class="col-md-4">
                            <a href="FeesParticulars.aspx">
                                <div class="card dash-widget border-card">
                                    <div class="card-body">
                                        <span class="dash-widget-icon"><i class="fas fa-server"></i></span>
                                        <div class="div-header">
                                            <span class="span-header font-color">Fee Particulars</span>
                                        </div>
                                    </div>
                                </div>
                            </a>
                        </div>
                 <% } %> 

                  <% if (checkStatus("feescollection",listmodules))
                    { %>
                    <div class="col-md-4">
                        <a href="FeesCollection.aspx">
                            <div class="card dash-widget border-card">
                                <div class="card-body">
                                    <span class="dash-widget-icon"><i class="fas fa-rupee-sign"></i></span>
                                    <div class="div-header">
                                        <span class="span-header font-color">Fees Collection</span>
                                    </div>
                                </div>
                            </div>
                        </a>
                    </div>
                <div class="col-md-4">
                    <a href="BulkRollNO.aspx">
                        <div class="card dash-widget border-card">
                            <div class="card-body">
                                <span class="dash-widget-icon"><i class="fas fa-user"></i></span>
                                <div class="div-header">
                                    <span class="span-header font-color">ROll No Updation</span>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>
                 <div class="col-md-4">
                    <a href="GRNoUpdation.aspx">
                        <div class="card dash-widget border-card">
                            <div class="card-body">
                                <span class="dash-widget-icon"><i class="fas fa-user"></i></span>
                                <div class="div-header">
                                    <span class="span-header font-color">GRNO Updation</span>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>
                <% } %> 

                 <% if (checkStatus("feereports",listmodules))
                    { %>
                <div class="col-md-4">
                    <a href="FeesCollectionReport.aspx">
                        <div class="card dash-widget border-card">
                            <div class="card-body">
                                <span class="dash-widget-icon"><i class="fas fa-file-download"></i></span>
                                <div class="div-header">
                                    <span class="span-header font-color">Fees Collection Report</span>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>

                 <div class="col-md-4">
                    <a href="FeesOutstandingReport.aspx">
                        <div class="card dash-widget border-card">
                            <div class="card-body">
                                <span class="dash-widget-icon"><i class="fas fa-file-download"></i></span>
                                <div class="div-header">
                                    <span class="span-header font-color">Fees Outstanding Report</span>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>
                <% } %> 


                 <% if (checkStatus("studentmaster",listmodules))
                     { %>
                  <div class="col-md-4">
                    <a href="StudentList.aspx">
                        <div class="card dash-widget border-card">
                            <div class="card-body">
                                <span class="dash-widget-icon"><i class="fas fa-user"></i></span>
                                <div class="div-header">
                                    <span class="span-header font-color">Student Master</span>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>

                  <div class="col-md-4">
                    <a href="LoginMaster.aspx">
                        <div class="card dash-widget border-card">
                            <div class="card-body">
                                <span class="dash-widget-icon"><i class="fas fa-user"></i></span>
                                <div class="div-header">
                                    <span class="span-header font-color">Login Master</span>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>
                 <div class="col-md-4">
                    <a href="ModuleMaster.aspx">
                        <div class="card dash-widget border-card">
                            <div class="card-body">
                                <span class="dash-widget-icon"><i class="fas fa-user"></i></span>
                                <div class="div-header">
                                    <span class="span-header font-color">Module Master</span>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>

                    <% } %> 

                 <% if (checkStatus("feescancel",listmodules))
                     { %>
                  <div class="col-md-4">
                    <a href="FeeCancel.aspx">
                        <div class="card dash-widget border-card">
                            <div class="card-body">
                                <span class="dash-widget-icon"><i class="fas fa-user"></i></span>
                                <div class="div-header">
                                    <span class="span-header font-color">Fees Cancel</span>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>

                

                 <div class="col-md-4">
                    <a href="../LCModule/LCDashboard.aspx" target="_blank">
                        <div class="card dash-widget border-card">
                            <div class="card-body">
                                <span class="dash-widget-icon"><i class="fas fa-user"></i></span>
                                <div class="div-header">
                                    <span class="span-header font-color">LC Module</span>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>
                 <div class="col-md-4">
                    <a href="../BonafideModule/BonafideDashboard.aspx" target="_blank">
                        <div class="card dash-widget border-card">
                            <div class="card-body">
                                <span class="dash-widget-icon"><i class="fas fa-user"></i></span>
                                <div class="div-header">
                                    <span class="span-header font-color">Bonafide Module</span>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>

                    <% } %> 
                  <% if (checkStatus("admissionmodule",listmodules))
                        { %>
                         <div class="col-md-4">
                                    <a href="../AdmissionModule/AdmissionForm.aspx" class="nav-link" id="menu_admission">
                                        <div class="card dash-widget border-card">
                            <div class="card-body">
                                <span class="dash-widget-icon"><i class="fas fa-user"></i></span>
                                <div class="div-header">
                                    <span class="span-header font-color">Admission Module</span>
                                </div>
                            </div>
                        </div>
                                    </a>
                               </div>
                     <% } %> 


            </div>
        </div>
    </section>


    <script>
        $(document).ready(function () {
            $("#menu_dash").addClass("active");
        });
    </script>
</asp:Content>
