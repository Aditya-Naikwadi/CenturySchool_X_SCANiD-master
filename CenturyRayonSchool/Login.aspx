<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CenturyRayonSchool.Login" %>

    <%--<link href="//maxcdn.bootstrapcdn.com/bootstrap/4.1.1/css/bootstrap.min.css" rel="stylesheet"
        id="bootstrap-css">--%>
        <script src="//maxcdn.bootstrapcdn.com/bootstrap/4.1.1/js/bootstrap.min.js"></script>
        <script src="//cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
        <!------ Include the above in your HEAD tag ---------->

        <!DOCTYPE html>
        <html>

        <head>
            <title>Login Page</title>
            <!--Made with love by Mutiullah Samim -->
            <meta name="viewport" content="width=device-width, initial-scale=1.0">

            <!--Bootsrap 4 CDN-->
            <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css"
                integrity="sha384-MCw98/SFnGE8fJT3GXwEOngsV7Zt27NXFoaoApmYm81iuXoPkFOJwJ8ERdknLPMO"
                crossorigin="anonymous">

            <!--Fontawesome CDN-->
            <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.3.1/css/all.css"
                integrity="sha384-mzrmE5qonljUremFsqc01SB46JvROS7bZs3IO2EmfFsd15uHvIt+Y8vEf7N7fWAU"
                crossorigin="anonymous">


            <style>
                /* Made with love by Mutiullah Samim*/

                @import url('https://fonts.googleapis.com/css?family=Numans');

                html,
                body {
                    /*background-image: url('../img/544750.jpg');*/
                    background-image: url('../img/front-view-jar-with-coins-academic-cap.jpg');
                    background-size: cover;
                    background-repeat: no-repeat;
                    background-position: center;
                    height: 100%;
                    font-family: 'Numans', sans-serif;
                    margin: 0;
                    padding: 0;
                }

                .container {
                    height: 100%;
                    align-content: center;
                }

                .card {
                    height: auto;
                    min-height: 390px;
                    margin-top: auto;
                    margin-bottom: auto;
                    width: 90%;
                    max-width: 400px;
                    background-color: rgba(0, 0, 0, 0.5) !important;
                }

                .social_icon span {
                    font-size: 60px;
                    margin-left: 10px;
                    color: #FFC312;
                }

                .social_icon span:hover {
                    color: white;
                    cursor: pointer;
                }

                .card-header h3 {
                    color: white;
                }

                .social_icon {
                    position: absolute;
                    right: 20px;
                    top: -45px;
                }

                .input-group-prepend span {
                    width: 50px;
                    background-color: #FFC312;
                    color: black;
                    border: 0 !important;
                }

                input:focus {
                    outline: 0 0 0 0 !important;
                    box-shadow: 0 0 0 0 !important;
                }

                .remember {
                    color: white;
                }

                .remember input {
                    width: 20px;
                    height: 20px;
                    margin-left: 15px;
                    margin-right: 5px;
                }

                .login_btn {
                    color: black;
                    background-color: #FFC312;
                    width: 100px;
                }

                .login_btn:hover {
                    color: black;
                    background-color: white;
                }

                .links {
                    color: white;
                }

                .links a {
                    margin-left: 4px;
                }

                header {
                    display: block;
                    padding: 10px;
                }

                .header h1 {
                    font-size: 1.8rem;
                }

                .header h3 {
                    font-size: 1rem;
                }

                @media (max-width: 576px) {
                    .header h1 {
                        font-size: 1.4rem;
                    }

                    .header h3 {
                        font-size: 0.8rem;
                    }
                }

                .card-footer {
                    padding: 2rem 1rem !important;
                }
            </style>

            <script type="text/javascript">

                $(document).ready(function () {

                    getLocalStorageValues();

                    $("#ckb1").on("click", function (e) {

                        if ($("#ckb1").is(":checked")) {
                            var iserror = false;
                            // save username and password
                            var Uname = $('#Uname').val();


                            if (Uname.length == 0) {
                                iserror = true;
                            }



                            if (iserror == false) {

                                localStorage.century_username = Uname;
                                /*localStorage.payroll_password = password;*/
                                localStorage.century_ckb1 = $('#ckb1').val();

                            }
                            else {
                                $('#ckb1').prop('checked', false);
                                $("#lblmessage").text("Kindly enter Emailid");
                                $("#alertmessagemodal").modal("show");

                            }

                        }
                        else {
                            localStorage.century_username = "";
                            /*localStorage.payroll_password = "";*/
                            localStorage.century_ckb1 = "";
                        }



                    });
                });

                function showAlertModal() {
                    var myModal = new bootstrap.Modal(document.getElementById('alertmessagemodal'))
                    myModal.show()


                }


                function getLocalStorageValues() {
                    var century_username = localStorage.getItem('century_username');
                    /* var payroll_password = localStorage.getItem('payroll_password');*/
                    var century_ckb1 = localStorage.getItem('century_ckb1');

                    if (century_ckb1 == "on") {
                        $('#Uname').val(century_username);
                        /*$('#txt_password').val(payroll_password);*/
                        $('#ckb1').attr('checked', true);
                    }
                }


            </script>

        </head>

        <body>
            <div class="container-fluid">
                <header id="header" class="header">

                    <div class="row">
                        <div class="col-md-12" style="padding-right: 0px; padding-left: 0px; text-align: center;">
                            <div class="text-container"
                                style="color: #FFC312; padding-top: 20px; padding-bottom: 10px; width: 100%; font-weight: 600;">

                                <h1>Century Rayon High School, Shahad </h1>
                                <h3 class="font-2">Kalyan - Ahmednagar Highway, Century Rayon Colony, Shahad,
                                    Ulhasnagar, Maharashtra 421103</h3>

                            </div>
                        </div>
                    </div>

                </header>

                <div class="d-flex justify-content-center h-100">
                    <div class="card">
                        <div class="card-header text-center">
                            <%--<h3 style="color: #FFC312;font-weight: 600;">Century Rayon High School, Shahad </h3>--%>


                                <h3>Sign In</h3>
                                <div class="d-flex justify-content-end social_icon">
                                    <%--<span><i class="fab fa-facebook-square"></i></span>
                                        <span><i class="fab fa-google-plus-square"></i></span>
                                        <span><i class="fab fa-twitter-square"></i></span>--%>
                                </div>
                        </div>
                        <div class="card-body">
                            <form runat="server">
                                <asp:Panel ID="LoginPanel" runat="server" DefaultButton="btn1_Login">
                                    <div class="input-group form-group">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text"><i class="fas fa-user"></i></span>
                                        </div>
                                        <%--<input type="text" class="form-control" placeholder="username">--%>
                                            <asp:TextBox ID="Uname" runat="server" CssClass="form-control"
                                                placeholder="username"></asp:TextBox>

                                    </div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                        ControlToValidate="Uname" ErrorMessage="Please Enter Your Username"
                                        ForeColor="Red" ValidationGroup="LoginGroup"></asp:RequiredFieldValidator>
                                    <div class="input-group form-group">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text"><i class="fas fa-key"></i></span>
                                        </div>
                                        <%--<input type="password" class="form-control" placeholder="password">--%>
                                            <asp:TextBox ID="Upass" runat="server" CssClass="form-control"
                                                TextMode="Password" placeholder="password"></asp:TextBox>

                                    </div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                        ControlToValidate="Upass" ErrorMessage="Please Enter Your Password"
                                        ForeColor="Red" ValidationGroup="LoginGroup"></asp:RequiredFieldValidator>
                                    <div class="row align-items-center remember">
                                        <input id="ckb1" type="checkbox">Remember Me
                                    </div>
                                    <div class="form-group">
                                        <%--<input type="submit" value="Login" class="btn float-right login_btn">--%>
                                            <asp:Button ID="btn1_Login" runat="server" Text="Login"
                                                CssClass="btn float-right login_btn" OnClick="btn1_Login_Click"
                                                ValidationGroup="LoginGroup" />
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="ForgotPasswordPanel" runat="server" Visible="false"
                                    DefaultButton="btnResetPassword">
                                    <div class="input-group form-group">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text"><i class="fas fa-user"></i></span>
                                        </div>
                                        <asp:TextBox ID="txtForgotUsername" runat="server" CssClass="form-control"
                                            placeholder="Enter your username"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                        ControlToValidate="txtForgotUsername" ErrorMessage="Please Enter Your Username"
                                        ForeColor="Red" ValidationGroup="ForgotPasswordGroup">
                                    </asp:RequiredFieldValidator>

                                    <div class="input-group form-group">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text"><i class="fas fa-key"></i></span>
                                        </div>
                                        <asp:TextBox ID="txtNewPassword" runat="server" CssClass="form-control"
                                            TextMode="Password" placeholder="New password"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                        ControlToValidate="txtNewPassword" ErrorMessage="Please Enter New Password"
                                        ForeColor="Red" ValidationGroup="ForgotPasswordGroup">
                                    </asp:RequiredFieldValidator>

                                    <div class="input-group form-group">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text"><i class="fas fa-key"></i></span>
                                        </div>
                                        <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control"
                                            TextMode="Password" placeholder="Confirm password"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                        ControlToValidate="txtConfirmPassword" ErrorMessage="Please Confirm Password"
                                        ForeColor="Red" ValidationGroup="ForgotPasswordGroup">
                                    </asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server"
                                        ControlToCompare="txtNewPassword" ControlToValidate="txtConfirmPassword"
                                        ErrorMessage="Passwords do not match" ForeColor="Red"
                                        ValidationGroup="ForgotPasswordGroup"></asp:CompareValidator>

                                    <div class="form-group" style="margin-top: -10px;">
                                        <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn login_btn"
                                            OnClick="btnBack_Click" CausesValidation="false" />
                                        <asp:Button ID="btnResetPassword" runat="server" Text="Reset"
                                            CssClass="btn float-right login_btn" OnClick="btnResetPassword_Click"
                                            ValidationGroup="ForgotPasswordGroup" />
                                    </div>
                                </asp:Panel>
                                <div class="card-footer">
                                    <div class="d-flex justify-content-center">
                                        <asp:LinkButton ID="lnkForgotPassword" runat="server"
                                            Text="Forgot your password?" ForeColor="White"
                                            OnClick="lnkForgotPassword_Click" CausesValidation="false" Visible="true">
                                        </asp:LinkButton>

                                    </div>
                                </div>

                            </form>
                        </div>
                    </div>
                </div>
            </div>

            <div class="modal" id="errorMessageModal">
                <div class="modal-dialog">
                    <div class="modal-content">

                        <!-- Modal Header -->
                        <div class="modal-header">
                            <h4 class="modal-title">Error Message</h4>
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                        </div>

                        <!-- Modal body -->
                        <div class="modal-body">
                            <asp:Label ID="errorMessage" runat="server" Style="font-weight: bold;"></asp:Label>
                        </div>

                        <!-- Modal footer -->
                        <div class="modal-footer">
                            <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                        </div>

                    </div>
                </div>
            </div>

            <div class="modal fade" id="alertmessagemodal" tabindex="-1" role="dialog"
                aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="exampleModalLabel" style="font-weight: bold;">Alert Message!!
                            </h5>

                            <img src="../img/alertimage.png" style="height: 100px; width: auto;" />
                        </div>
                        <div class="modal-body">
                            <asp:Label Text="" runat="server" ID="lblalertmessage" Style="font-weight: normal;" />
                            <%--<label id="lblmessage" style="font-weight: normal;"></label>--%>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-danger" data-dismiss="modal">OK</button>
                        </div>
                    </div>
                </div>
            </div>



        </body>

        </html>