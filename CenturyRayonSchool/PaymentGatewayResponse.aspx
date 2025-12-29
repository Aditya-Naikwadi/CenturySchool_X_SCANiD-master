<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaymentGatewayResponse.aspx.cs" Inherits="CenturyRayonSchool.PaymentGatewayResponse" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="stylesheet" href="~/ParentsModule/css/site.css" />
    <link rel="shortcut icon" type="/image/x-icon" href="/img/favicon.png" />
    <!-- Normalize CSS -->
    <link rel="stylesheet" href="~/ParentsModule/css/normalize.css"/>
    <!-- Main CSS -->
    <link rel="stylesheet" href="~/ParentsModule/css/main.css"/>
    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="~/ParentsModule/css/bootstrap.min.css"/>
    <link href="~/ParentsModule/css/bootstrap-datetimepicker.min.css" rel="stylesheet" />
    <!-- Fontawesome CSS -->
    <link rel="stylesheet" href="~/ParentsModule/css/all.min.css"/>
    <!-- Flaticon CSS -->
    <link rel="stylesheet" href="~/ParentsModule/fonts/flaticon.css"/>
    <!-- Full Calender CSS -->
    <link rel="stylesheet" href="~/ParentsModule/css/fullcalendar.min.css"/>
    <!-- Animate CSS -->
    <link rel="stylesheet" href="~/ParentsModule/css/animate.min.css"/>
    <!-- Data Table CSS -->
    <link rel="stylesheet" href="~/ParentsModule/css/jquery.dataTables.min.css"/>
    <!-- Select 2 CSS -->
    <link rel="stylesheet" href="~/ParentsModule/css/select2.min.css"/>
    <!-- Date Picker CSS -->
    <link rel="stylesheet" href="~/ParentsModule/css/datepicker.min.css"/>
    <!-- Custom CSS -->
    <link rel="stylesheet" href="~/ParentsModule/css/style.css"/>
    <!-- jquery -->
    <script src="~/ParentsModule/js/jquery-3.3.1.min.js"></script>
    <!-- Modernize js -->
    <script src="~/ParentsModule/js/modernizr-3.6.0.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.1/css/all.min.css"/>
    <title>PaymentGateway Response</title>
    <style>
        body {
            overflow: hidden;
        }
        .header-menu-one {
            background: linear-gradient(to right, #fff, #ffaa01);
            justify-content: center !important;
            font-size: xx-large;
            color: darkmagenta;
        }
        .content {
            text-align: center;
            margin-top: 10px;
        }
        @media (max-width: 767px) {
            .header-menu-one {
                font-size: large;
            }
            .content .card {
                width: 100%;
                margin: 0;
            }
            .btn-lg {
                font-size: medium;
                padding: 10px 20px;
            }
            .navbar {
                flex-direction: column;
                align-items: center;
            }
        }
        @media (min-width: 768px) {
            .content .card {
                width: auto;
            }
            .btn-lg {
                font-size: x-large;
                padding: 15px 30px;
            }
        }
    </style>
</head>
<body>
    <div style="display:none">
      <asp:Label Text="Response Code :" runat="server" />
        <asp:Label Text="-" runat="server" ID="lbl_response_code" />
        <asp:Label Text="-" runat="server" ID="lbl_status" />
         <asp:Label Text="Reference No. :" runat="server" />
         <asp:Label Text="-" runat="server" ID="lbl_referenceno" />
        <asp:Label Text="Paid  Amount :" runat="server" />
        <asp:Label Text="-" runat="server" ID="lbl_paidamnt" />
        <asp:Label Text="Transaction Date" runat="server" />
        <asp:Label Text="-" runat="server" ID="lbl_TransactionDate" />
         <asp:Label Text="STD - DIV" runat="server" />
        <asp:Label Text="-" runat="server" ID="lbl_stddiv" />
         <asp:Label Text=" Grno" runat="server" />
        <asp:Label Text="-" runat="server" ID="lbl_grno" />
        <asp:Label Text="Year" runat="server" />
        <asp:Label Text="-" runat="server" ID="lbl_year" />
        <asp:Label Text="Signature" runat="server" />
        <asp:Label Text="-" runat="server" ID="lbl_signature" />
        <asp:Label Text="Paymode" runat="server" />
        <asp:Label Text="-" runat="server" ID="lbl_paymode" />
        <asp:Label Text="Transection No" runat="server" />
        <asp:Label Text="-" runat="server" ID="lbl_transectionid" />
        <asp:Label Text="StudentName " runat="server" />
        <asp:Label Text="-" runat="server" ID="lbl_studentname" />
             <asp:Label Text="Error" runat="server" />
        <asp:Label Text="-" runat="server" ID="Lbl_error" />
    </div>
    <form id="form1" runat="server">
        <div class="navbar navbar-expand-md header-menu-one bg-light">
            <asp:Label runat="server">CENTURY RAYON HIGH SCHOOL SHAHAD</asp:Label>
        </div>
        <div class="container-fluid" style="display: flex; justify-content: center; align-items: center; height: 100vh;">
            <section class="content">
                <div class="card dashboard-card-one pd-b-20">
                    <div class="card-body">
                        <div class="row gutters-20">
                            <div class="col-12" style="font-size: xxx-large; font-family: ui-sans-serif; color: rebeccapurple;">
                                <asp:Label Text="TRANSACTION SUCCESSFUL !!!" ID="lbl_success" runat="server" Visible="false"/>
                                <asp:Label Text="TRANSACTION FAILED !!!" ID="lbl_Fail" runat="server" Visible="false"/>
                            </div>
                        </div>
                        <div class="row gutters-20">
                            <div class="col-md-6">
                                <asp:Button runat="server" id="printreceipt" class="btn btn-success btn-hover-bluedark btn-lg" OnClick="printreceipt_Click" Text="Print Receipt" Font-Size="XX-Large" Visible="false"/>
                            </div>
                            <div class="col-md-6">
                                <asp:Button runat="server" id="Goto_dashboard" class="btn btn-danger btn-hover-bluedark btn-lg" OnClick="Goto_dashboard_Click" Text="Go to Dashboard" Font-Size="XX-Large" Visible="false"/>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        </div>
    </form>

    <div class="modal fade" id="alertmessagemodal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" style="font-weight: bold;">Alert Message!!</h5>
                    <img src="../img/alertimage.png" style="height:100px;width:auto;" />
                </div>
                <div class="modal-body">
                    <asp:Label Text="" runat="server" ID="lblalertmessage" style="font-weight: normal;" />
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">OK</button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="infomessagemodal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" style="font-weight: bold;">Information Message.</h5>
                    <img src="../img/information.png" style="height:100px;width:auto;" />
                </div>
                <div class="modal-body">
                    <asp:Label Text="" runat="server" ID="lblinfomsg" style="font-weight: normal;" />
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-success" data-dismiss="modal">OK</button>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function showInfoModal() {
            var myModal = new bootstrap.Modal(document.getElementById('infomessagemodal'));
            myModal.show();
        }

        function showAlertModal() {
            var myModal = new bootstrap.Modal(document.getElementById('alertmessagemodal'));
            myModal.show();
        }
    </script>
</body>
</html>
