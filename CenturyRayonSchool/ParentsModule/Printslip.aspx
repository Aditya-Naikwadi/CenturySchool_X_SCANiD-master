<%@ Page Title="" Language="C#" MasterPageFile="~/ParentsModule/ParentModule.Master" AutoEventWireup="true" CodeFile="Printslip.aspx.cs" Inherits="CenturyRayonSchool.ParentsModule.Printslip" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .container {
            padding: 10px;
        }

        .btn {
            display: block;
            width: 100%;
            margin-bottom: 10px;
        }

        .responsive-iframe {
            position: relative;
            overflow: hidden;
            width: 100%;
            padding-top: 56.25%; /* 16:9 aspect ratio */
        }

            .responsive-iframe iframe {
                position: absolute;
                top: 0;
                left: 0;
                width: 100%;
                height: 100%;
            }
    </style>
    <div class="container">
        <a id="dashpage" href="#" class="btn btn-warning btn-lg" onclick="goToDashboard()">Go to Dashboard</a>
        <div class="responsive-iframe">
            <iframe src="<%=fileurl%>" frameborder="0" allowfullscreen></iframe>
        </div>
    </div>
    <script>
        function goToDashboard() {
            // Redirect to the dashboard page
            window.location.href = 'ParentsDashboard.aspx';
        }
    </script>
</asp:Content>
