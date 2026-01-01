<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ParentsForgotPassword.aspx.cs" Inherits="CenturyRayonSchool.ParentsModule.ParentsForgotPassword" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reset Password</title>
    <script src="../ParentsModule/js/jquery-3.3.1.min.js"></script>
    <link href="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/css/select2.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/js/select2.min.js"></script>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/css/bootstrap.min.css" />

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/js/bootstrap.bundle.min.js"></script>

    <style>
        @import url('https://fonts.googleapis.com/css?family=Raleway:400,700');

        * {
            box-sizing: border-box;
            margin: 0;
            padding: 0;
            font-family: Raleway, sans-serif;
        }

        body {
            overflow: hidden;
            /*background: linear-gradient(90deg, #C7C5F4, #a31836);*/
            background-color: #005b39;
        }

        .container {
            display: flex;
            align-items: center;
            justify-content: center;
            min-height: 80vh;
        }

        .screen {
            background: linear-gradient(90deg, #65d1a9, #2b614d);
            position: relative;
            height: 450px;
            width: 450px;
            box-shadow: 0px 0px 24px #49cb9a;
        }

        .screen__content {
            z-index: 1;
            position: relative;
            height: 100%;
        }

        .screen__background {
            position: absolute;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            z-index: 0;
            -webkit-clip-path: inset(0 0 0 0);
            clip-path: inset(0 0 0 0);
        }

        .screen__background__shape {
            transform: rotate(45deg);
            position: absolute;
        }

        .screen__background__shape1 {
            height: 520px;
            width: 520px;
            background: #FFF;
            top: -50px;
            right: 120px;
            border-radius: 0 72px 0 0;
        }

        .screen__background__shape2 {
            height: 220px;
            width: 220px;
            background: #57a689;
            top: -172px;
            right: 0;
            border-radius: 32px;
        }

        .screen__background__shape3 {
            height: 540px;
            width: 190px;
            background: linear-gradient(270deg, #237959, #2bac7c);
            top: -24px;
            right: 0;
            border-radius: 32px;
        }

        .screen__background__shape4 {
            height: 400px;
            width: 200px;
            background: #3b8f6f;
            top: 420px;
            right: 50px;
            border-radius: 60px;
        }

        .login {
            width: 320px;
            padding: 30px;
            padding-top: 156px;
        }


        .login__field {
            padding: 10px 0px;
            position: relative;
            text-align: center;
        }

        .login__field2 {
            padding: 20px 0px;
            position: relative;
        }

        .login__field3 {
            padding: 0px 0px;
            position: relative;
        }

            .login__field3 > a {
                text-decoration: none;
                color: #005b39;
            }

                .login__field3 > a:hover {
                    text-decoration: none;
                    color: #0c271d;
                }

        .login__icon {
            position: absolute;
            top: 30px;
            color: #7875B5;
        }

        .login__input {
            border: none;
            border-bottom: 2px solid #D1D1D4;
            background: none;
            padding: 10px;
            padding-left: 24px;
            font-weight: bolder;
            width: 75%;
            transition: .2s;
            font-size: 20px;
        }

            .login__input:active,
            .login__input:focus,
            .login__input:hover {
                outline: none;
                border-bottom-color: #005b39;
            }

        .login__submit {
            background: #fff;
            font-size: 14px;
            margin-top: 3px;
            padding: 16px 150px;
            border-radius: 26px;
            border: 1px solid #D4D3E8;
            text-transform: uppercase;
            font-weight: 900;
            display: flex;
            align-items: center;
            width: 100%;
            color: #005b39;
            box-shadow: 0px 2px 2px #1e996b;
            cursor: pointer;
            transition: .2s;
        }

            .login__submit:active,
            .login__submit:focus,
            .login__submit:hover {
                border-color: #1e996b;
                outline: none;
            }

        .button__icon {
            font-size: 24px;
            margin-left: auto;
            color: #7875B5;
        }

        .social-login {
            position: absolute;
            height: 140px;
            width: 160px;
            text-align: center;
            bottom: 0px;
            right: 0px;
            color: #fff;
        }

        .social-icons {
            display: flex;
            align-items: center;
            justify-content: center;
        }

        .social-login__icon {
            padding: 20px 10px;
            color: #fff;
            text-decoration: none;
            text-shadow: 0px 0px 8px #7875B5;
        }

            .social-login__icon:hover {
                transform: scale(1.5);
            }

        header {
            display: block;
        }

        .pageName {
            color: #ffc312;
            /*border:1px solid black;*/
            text-align: center;
            position: relative;
            top: 50px;
            font-size: 25px;
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

        @media screen and (max-width: 768px) {
            .container {
                padding: 0 10px;
            }

            .screen {
                width: 90%;
            }

            .login {
                padding: 30px 10px;
                padding-top: 156px;
            }

            .login__input {
                width: 100%;
            }

            .login__submit {
                width: 100%;
            }
        }

        @media screen and (max-width: 576px) {
            .login {
                padding-top: 120px;
            }

            .pageName {
                font-size: 20px;
            }
        }
    </style>
</head>
<body>

    <form id="form1" runat="server">

        <div class="container-fluid">
            <header id="header" class="header">

                <div class="row">
                    <div class="col-md-12" style="padding-right: 0px; padding-left: 0px; text-align: center;">
                        <div class="text-container" style="color: #FFC312; padding-top: 20px; padding-bottom: 10px; width: 100%; font-weight: 600;">

                            <h1 style="font-size: 60px;">Century Rayon High School, Shahad </h1>
                            <h3 class="font-2" style="font-size: 25px;">Kalyan - Ahmednagar Highway, Century Rayon Colony, Shahad, Ulhasnagar, Maharashtra 421103</h3>

                        </div>
                    </div>
                </div>

            </header>

            <h3 class="pageName">Forgot Password</h3>
            <div class="container" id="MainDiv">
                <div class="screen">
                    <div class="screen__content">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="login__field">
                                    <i class="login__icon fas fa-user"></i>
                                    <asp:TextBox runat="server" type="text" class="login__input" ID="Academic" placeholder="Academic Year" ReadOnly></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Style="top: 65px; left: 26px; position: absolute; color: red; height: 26px; width: 162px" ErrorMessage="AcademicYear Required" ControlToValidate="Academic"></asp:RequiredFieldValidator>
                                </div>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="login__field" style="margin-left: 45px">
                                    <asp:DropDownList ID="cmbStd" class="select" runat="server">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="cmbStderror" InitialValue="Select Std" runat="server" ErrorMessage="Please Select Standard" ControlToValidate="cmbStd" ForeColor="Red"></asp:RequiredFieldValidator>

                                    <%--                                    <asp:CustomValidator ID="stdCustomvalid" runat="server" ErrorMessage="Select Std" OnServerValidate="stdCustomvalid_ServerValidate" ControlToValidate="cmbStd" ForeColor="Red"></asp:CustomValidator>--%>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="login__field">
                                    <i class="login__icon fas fa-user"></i>
                                    <asp:TextBox runat="server" type="text" ID="GRNo" class="login__input" placeholder="GR No"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Style="top: 65px; left: 26px; position: absolute; color: red; height: 26px; width: 162px" ErrorMessage="GRNO Required" ControlToValidate="GRNo"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="login__field">
                                    <i class="login__icon fas fa-lock"></i>
                                    <asp:TextBox runat="server" type="password" class="login__input" ID="pwd" placeholder="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Style="top: 65px; left: 26px; position: absolute; color: red; height: 26px; width: 162px" ErrorMessage="Password Required" ControlToValidate="GRNo"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="login__field">
                                    <i class="login__icon fas fa-lock"></i>
                                    <asp:TextBox runat="server" type="password" class="login__input" ID="Confirmpwd"  placeholder="Confirm Password"></asp:TextBox>
                                    <asp:Label runat="server" ID="Pwd_NotMatch"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Style="top: 65px; left: 26px; position: absolute; color: red; height: 26px; width: 201px;" ErrorMessage="Confirm Password Required" ControlToValidate="Confirmpwd"></asp:RequiredFieldValidator>

                                </div>
                                <div class="login__field" style="text-align: left">
                                    <asp:TextBox runat="server" ID="showPassword" Style="margin-left: 10px; margin-bottom: 0px;" type="checkbox" class="login__input_ShowPassword" onchange="ShowPassword()"></asp:TextBox><asp:Label Style="padding-left: 10px; margin-bottom: 0px;" runat="server">Show Password</asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group" style="text-align: -webkit-center;">
                                    <asp:Button ID="btn_ChangePassword" runat="server" Text="Change Password" CssClass="button login__submit" OnClick="btn_ChangePassword_Click" />
                                </div>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-md-12">
                            </div>
                        </div>

                    </div>
                    <div class="screen__background">
                        <span class="screen__background__shape screen__background__shape4"></span>
                        <span class="screen__background__shape screen__background__shape3"></span>
                        <span class="screen__background__shape screen__background__shape2"></span>
                        <span class="screen__background__shape screen__background__shape1"></span>
                    </div>
                </div>
            </div>
        </div>

    </form>

    <div class="modal fade" id="alertmessagemodal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" style="font-weight: bold;">Alert Message!!</h5>

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
<script type="text/javascript">
    $(document).ready(function (e) {
        $('.select').select2();
    });
    function showAlertModal() {
        var myModal = new bootstrap.Modal(document.getElementById('alertmessagemodal'))
        myModal.show()
    }
    function ShowPassword() {
        var textBox = document.getElementById('<%= showPassword.ClientID %>');
        if (textBox.checked) {
            document.getElementById('<%= pwd.ClientID %>').type = "text";
            document.getElementById('<%= Confirmpwd.ClientID %>').type = "text";
        } else {
            document.getElementById('<%= pwd.ClientID %>').type = "password";
            document.getElementById('<%= Confirmpwd.ClientID %>').type = "password";
        }
    }
    function showInfoModal() {
        var myModal = new bootstrap.Modal(document.getElementById('infomessagemodal'))
        myModal.show()
    }


</script>
</html>
