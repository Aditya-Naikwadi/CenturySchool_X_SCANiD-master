<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="CenturyRayonSchool.ParentsModule.WebForm1" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Parent's Login</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <!-- Bootstrap -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css" />
    <link href="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/css/select2.min.css" rel="stylesheet" />
    <style>
        body {
            /*font-family: 'Segoe UI', sans-serif;*/
            /*background: linear-gradient(to right, #D8B5FF, #1EAE98);*/
            min-height: 100vh;
        }


        * {
            box-sizing: border-box;
            margin: 0;
            padding: 0;
            font-family: Raleway, sans-serif;
        }

        .login__input {
            border: none;
            border-bottom: 2px solid #D1D1D4;
            background: none;
            padding: 10px;
            padding-left: 8rem;
            font-weight: bolder;
            width: 100%;
            transition: .2s;
            font-size: 20px;
            margin: -10px;
            outline: none;
        }


        body {
            overflow: hidden;
            background: linear-gradient(#D8B5FF, #1EAE98);
            /* background-color: #005b39;*/
        }

        .login-container {
            max-width: 420px;
            margin: 60px auto;
            background: #ffffffcc;
            padding: 30px 25px;
            border-radius: 15px;
            box-shadow: 0 8px 25px rgba(0,0,0,0.2);
        }

        .login__field {
            left: 10px;
            position: inherit;
            text-align: center;
        }

        .login-title {
            font-weight: 700;
            text-align: center;
            color: #005b39;
        }

        .form-icon {
            position: absolute;
            top: 50%;
            left: 15px;
            transform: translateY(-50%);
            color: #1EAE98;
        }

        .input-group {
            position: relative;
        }

        .form-control {
            padding-left: 40px;
        }

        .btn-primary {
            background-color: #1EAE98;
            border: none;
        }

            .btn-primary:hover {
                background-color: #178d7a;
            }

        .select {
            width: 100% !important;
        }

        .select2-container--default .select2-selection--single {
            height: 38px;
            padding: 6px 12px;
            border: 1px solid #ced4da;
            border-radius: 0.25rem;
        }

        .select2-container--default .select2-results__options {
            max-height: 200px;
        }

        .select2-dropdown {
            top: 100% !important;
        }
          @media screen and (max-device-width: 1174px) {
            .SclName {
                font-size: 40px !important;
            }

            .font-2 {
                font-size: 19px !important;
            }
        }

        @media screen and (device-width:1024px) {
            .SclName {
                font-size: 40px !important;
            }

            .font-2 {
                font-size: 19px !important;
            }

            .pageName {
                top: 6px;
            }
        }
    </style>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/js/select2.min.js"></script>

    <script type="text/javascript">
        function showAlertModal() {
            var showModal = new bootstrap.Modal(document.getElementById('alertmessagemodal'));
            showModal.show();
        }


    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" ID="ScriptManager1" />

        <div class="container">
           
                 <div class="row">
                <div class="col-md-12" style="padding-right: 0px; padding-left: 0px; text-align: center;">
                    <div class="text-container" style="color: #001070cc; padding-top: 20px; padding-bottom: 10px; width: 100%; font-weight: 600;">
                        <h1 class="SclName" style="font-size: 60px;">Century Rayon High School, Shahad </h1>
                        <h3 class="font-2 scladdress" style="font-size: 25px;">Kalyan - Ahmednagar Highway, Century Rayon Colony, Shahad, Ulhasnagar, Maharashtra 421103</h3>
                    </div>
                </div>
            </div>
            <asp:Panel ID="pnlFormError" runat="server" CssClass="alert alert-warning alert-dismissible fade show" Visible="false" role="alert">
                <asp:Label ID="lblFormError" runat="server" Text="Please fill in all required fields." />
                <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
            </asp:Panel>

            <div class="login-container">
                <h3 class="login-title mb-4">Parent's Login</h3>

                <!-- Academic Year -->
                <div class="mb-3 input-group">
                    <div class="login__field w-100">
                        <label class="form-label fw-semibold">Academic Year <span class="text-danger"></span></label>
                        <i class="fas fa-user" style="left: 50px;"></i>
                        <asp:TextBox runat="server" type="text" class="login__input" ID="Academicyear" placeholder="Academic Year" ReadOnly></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                            CssClass="text-danger form-text"
                            ErrorMessage="Academic Year is required"
                            ControlToValidate="Academicyear" Display="Dynamic" />
                    </div>
                </div>

                <!-- Select Std -->
                <div class="mb-3 mt-4">

                    <asp:DropDownList ID="cmbStd" runat="server" CssClass="form-select select" AutoPostBack="true"></asp:DropDownList>
                    <asp:CustomValidator ID="stdCustomvalid" runat="server"
                        CssClass="text-danger form-text"
                        ErrorMessage="Please select a standard"
                        OnServerValidate="stdCustomvalid_ServerValidate"
                        ControlToValidate="cmbStd" Display="Dynamic" />
                </div>

                <!-- GR No -->
                <div class="mb-3 position-relative">
                    <i class="fas fa-id-card position-absolute" style="top: 50%; left: 12px; transform: translateY(-50%); color: #1EAE98;"></i>
                    <asp:TextBox runat="server" ID="GRNo" CssClass="form-control ps-5" placeholder="Enter GR No" />
                </div>

                <!-- Password -->
                <div class="mb-3 position-relative">
                    <i class="fas fa-lock position-absolute" style="top: 50%; left: 12px; transform: translateY(-50%); color: #1EAE98;"></i>
                    <asp:TextBox runat="server" ID="pwd" CssClass="form-control ps-5" TextMode="Password" placeholder="Enter Password" />
                </div>

                <!-- Show Password -->
                <div class="form-check mb-3">
                    <input type="checkbox" class="form-check-input" id="chkShowPassword" onclick="togglePassword()" />
                    <label class="form-check-label" for="chkShowPassword">Show Password</label>
                </div>

                <!-- Login Button -->
                <div class="d-grid mb-3">
                    <asp:Button ID="btn1_Login" runat="server" Text="Log in" CssClass="btn btn-primary" OnClick="btn1_Login_Click" />
                </div>

                <!-- Remember Me -->
                <div class="form-check mb-2">
                    <asp:CheckBox runat="server" ID="rememberMeCheckbox" CssClass="form-check-input" />
                    <label class="form-check-label" for="rememberMeCheckbox">Remember Me</label>
                </div>

                <!-- Forgot Password -->
                <div class="text-end">
                    <a href="ParentsForgotPassword.aspx" class="text-decoration-none fw-semibold">Forgot Password?</a>
                </div>
            </div>

        </div>
        <div class="modal fade" id="alertmessagemodal" tabindex="-1" aria-labelledby="alertModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header d-flex align-items-center justify-content-between">
                        <h5 class="modal-title fw-bold" id="alertModalLabel">Alert !</h5>
                        <div class="d-flex align-items-center">
                            <img src="../img/alertimage.png" style="height: 50px; width: auto;" />
                            <button type="button" class="btn-close ms-2" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                    </div>

                    <div class="modal-body">
                        <asp:Label Text="" runat="server" ID="lblalertmessage" CssClass="text-danger fw-semibold" />
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger" data-bs-dismiss="modal">OK</button>
                    </div>
                </div>
            </div>
        </div>

    </form>


    <script>
        $(document).ready(function () {
            $('#<%= cmbStd.ClientID %>').select2({
                dropdownParent: $('.login-container'),
                placeholder: 'Select Standard',
                allowClear: true
            });
        });
        $(document).ready(function (e) {
            $('.select').select2();
        });
        function togglePassword() {
            var pwd = document.getElementById('<%= pwd.ClientID %>');
            pwd.type = (pwd.type === "password") ? "text" : "password";
        }
    </script>





</body>
</html>
