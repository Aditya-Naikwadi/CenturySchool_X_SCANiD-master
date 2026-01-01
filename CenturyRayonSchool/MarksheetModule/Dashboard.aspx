<%@ Page Title="" Language="C#" MasterPageFile="~/MarksheetModule/Marksheet.Master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="CenturyRayonSchool.MarksheetModule.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .font-color {
            color: navy !important;
        }

        .uppercase {
            text-transform: uppercase;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-header">
        <div class="row">
            <div class="col-sm-10">
                <h3 class="page-title fs-16">Welcome, &nbsp
                    <asp:Label runat="server" Text="Insiya Kanchwala" ID="lbldisplayusername" CssClass="uppercase"></asp:Label></h3>

            </div>
            <div class="col-sm-2">
                <asp:Label Text="Academic Year :" runat="server" />
                <h3 class="page-title fs-16">
                    <asp:Label runat="server" ID="lblAcademicyear"></asp:Label>

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
            <% if (checkStatus("Master", listmodules))
                { %>
            <div class="row">
                <div class="col-md-4">
                    <a href="SubjectList.aspx">
                        <div class="card dash-widget border-card">
                            <div class="card-body">
                                <span class="dash-widget-icon"><i class="fas fa-list"></i></span>
                                <div class="div-header">
                                    <span class="span-header font-color">Subject's List</span>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>

                <div class="col-md-4">
                    <a href="ExamMaster.aspx">
                        <div class="card dash-widget border-card">
                            <div class="card-body">
                                <span class="dash-widget-icon"><i class="fas fa-server"></i></span>
                                <div class="div-header">
                                    <span class="span-header font-color">Exam Master</span>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>

                <div class="col-md-4">
                    <a href="SubjectMaster.aspx">
                        <div class="card dash-widget border-card">
                            <div class="card-body">
                                <span class="dash-widget-icon"><i class="fas fa-rupee-sign"></i></span>
                                <div class="div-header">
                                    <span class="span-header font-color">Subject Master</span>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>
                <div class="col-md-4">
                    <a href="WorkingDays.aspx">
                        <div class="card dash-widget border-card">
                            <div class="card-body">
                                <span class="dash-widget-icon"><i class="fas fa-user"></i></span>
                                <div class="div-header">
                                    <span class="span-header font-color">Working Days</span>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>

                <div class="col-md-4">
                    <a href="TeacherMapping.aspx">
                        <div class="card dash-widget border-card">
                            <div class="card-body">
                                <span class="dash-widget-icon"><i class="fas fa-user"></i></span>
                                <div class="div-header">
                                    <span class="span-header font-color">Teacher's Mapping</span>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>
            </div>
            <% } %>

            <% if (checkStatus("Marksentry", listmodules))
                { %>
            <div class="row">
                <div class="col-md-4">
                    <a href="SubjectMarks.aspx">
                        <div class="card dash-widget border-card">
                            <div class="card-body">
                                <span class="dash-widget-icon"><i class="fas fa-file-download"></i></span>
                                <div class="div-header">
                                    <span class="span-header font-color">Subject Marks Entry 1 to 8</span>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>
                <% } %>
                <% if (checkStatus("Marksentry9", listmodules))
                    { %>
                <div class="col-md-4">
                    <a href="SubjectMarks8to9.aspx">
                        <div class="card dash-widget border-card">
                            <div class="card-body">
                                <span class="dash-widget-icon"><i class="fas fa-file-download"></i></span>
                                <div class="div-header">
                                    <span class="span-header font-color">Subject Marks Entry 9th</span>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>
                <% } %>

                <% if (checkStatus("Printing", listmodules))
                    { %>
                <div class="col-md-4">
                    <a href="PrintMarksheet.aspx">
                        <div class="card dash-widget border-card">
                            <div class="card-body">
                                <span class="dash-widget-icon"><i class="fas fa-user"></i></span>
                                <div class="div-header">
                                    <span class="span-header font-color">Print Marksheet</span>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>
                <% } %>
                <%--<div class="col-md-4">
                    <a href="RollNoUpdate.aspx">
                        <div class="card dash-widget border-card">
                            <div class="card-body">
                                <span class="dash-widget-icon"><i class="fas fa-user"></i></span>
                                <div class="div-header">
                                    <span class="span-header font-color">Bulk RollNO Update</span>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>--%>
                <% if (checkStatus("Remark_Att", listmodules))
                    { %>
                <div class="col-md-4">
                    <a href="Remark.aspx">
                        <div class="card dash-widget border-card">
                            <div class="card-body">
                                <span class="dash-widget-icon"><i class="fas fa-user"></i></span>
                                <div class="div-header">
                                    <span class="span-header font-color">Remark's</span>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>

                <div class="col-md-4">
                    <a href="AttendanceMark.aspx">
                        <div class="card dash-widget border-card">
                            <div class="card-body">
                                <span class="dash-widget-icon"><i class="fas fa-user"></i></span>
                                <div class="div-header">
                                    <span class="span-header font-color">Attendance Marking</span>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>
                <% } %>
            </div>



            
            <div class="row">
                <% if (checkStatus("ReportPrimary", listmodules))
                { %>
                <div class="col-md-4">
                    <a href="ConsolidationReport1to8.aspx">
                        <div class="card dash-widget border-card">
                            <div class="card-body">
                                <span class="dash-widget-icon"><i class="fas fa-user"></i></span>
                                <div class="div-header">
                                    <span class="span-header font-color">Consolidation Report 1to8 </span>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>
                <% } %>
                <% if (checkStatus("Report9", listmodules))
                    { %>
                <div class="col-md-4">
                    <a href="Consolidate9th.aspx">
                        <div class="card dash-widget border-card">
                            <div class="card-body">
                                <span class="dash-widget-icon"><i class="fas fa-user"></i></span>
                                <div class="div-header">
                                    <span class="span-header font-color">Consolidate Report 9th</span>
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
