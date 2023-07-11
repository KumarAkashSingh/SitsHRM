<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" type="text/css" href="Login/css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="Login/css/custom.css" />
    <link rel="stylesheet" type="text/css" href="Login/css/animate-custom.css" />
    <link rel="stylesheet" type="text/css" href="Login/css/font-awesome.min.css" />
    <title>Login page</title>
    <style>
        .modalp {
            position: fixed;
            z-index: 9999;
            height: 100%;
            width: 100%;
            top: 0;
            filter: alpha(opacity=60);
            opacity: 0.6;
            -moz-opacity: 0.8;
        }

        .centerp {
            position: inherit;
            top: 50%;
            left: 50%;
            right: 50%;
            bottom: 50%;
            height: 50%;
            width: 50%;
            margin: -32PX 0 0 -32PX;
            z-index: 10000;
            width: 150px;
            filter: alpha(opacity=100);
        }

            .centerp img {
                height: 64px;
                width: 64px;
            }
    </style>
</head>
<body class="bg-img" style="background-image: url('images/bg-01.jpg');">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdateProgress runat="server" ID="updateprocess">
            <ProgressTemplate>
                <div class="modalp">
                    <div class="centerp">
                        <img alt="" src="images/inprogress.gif" />
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel runat="server" ID="updatepanel">
            <ContentTemplate>
                <section class="form-sec">
                    <div class="container-fluid">
                        <div class="row">
                            <a class="hiddenanchor" id="toregister"></a>
                            <a class="hiddenanchor" id="tologin"></a>

                            <div id="forms" class="col-sm-12 col-xs-12">
                                <div id="login" class="bg-white animate form">
                                    <div class="text-center">
                                        <h3 class="heading">Login</h3>
                                    </div>

                                    <div class="form-icon">
                                        <img src="images/Employee2.png" alt="img-icon" class="tagline">
                                    </div>

                                    <div>
                                        <div class="form-group">
                                            <asp:TextBox type="text" runat="server" ID="txtLoginID" name="" class="form-control input_user" value="" placeholder="username" />
                                        </div>
                                        <div class="form-group">
                                            <asp:TextBox type="password" runat="server" ID="txtPassword" name="" class="form-control input_pass" value="" placeholder="password" />
                                        </div>
                                        <div class="form-group">
                                            <div class="custom-control custom-checkbox">
                                                <asp:CheckBox type="checkbox" class="custom-control-input" runat="server" ID="customControlInline" />
                                                <label class="custom-control-label" for="customControlInline">Remember me</label>
                                            </div>
                                            <div class="links">
                                                <a href="#toregister">Forget Password</a>
                                            </div>
                                        </div>
                                        <div class="login_container">
                                            <asp:Button type="submit" runat="server" ID="btnLogin" Text="Login" OnClick="btnLogin_Click" class="btn btn-danger"></asp:Button>

                                            <button type="submit" class="btn btn-dark"><i class="fa fa-play" aria-hidden="true"></i>Google Play</button>
                                        </div>
                                    </div>
                                </div>
                                <!--  -->
                                <div id="register" class="bg-white animate form">
                                    <div class="text-center">
                                        <h3 class="heading">Forget Password</h3>
                                    </div>
                                    <div class="form-icon">
                                        <img src="images/Employee2.png" alt="img-icon" class="tagline">
                                    </div>
                                    <form>
                                        <div class="form-group">
                                            <select name="ddlType" id="ddlType" class="chosen-select form-control">
                                                <option selected="selected" value="0">Select Type</option>
                                                <option value="1">Employee</option>
                                                <option value="2">Student</option>
                                            </select>
                                        </div>
                                        <div class="form-group">
                                            <input type="text" name="" class="form-control input_user" value="" placeholder="username">
                                        </div>

                                        <div class="login_container">
                                            <button type="submit" class="btn btn-danger btn-block">Send Me! <i class="fa fa-paper-plane" aria-hidden="true"></i></button>

                                        </div>
                                        <div class="col-sm-12">
                                            <div class="text-center">
                                                <a style="color: #666; font-size: 15px;" href="#tologin">Back to login </a>
                                            </div>
                                        </div>
                                    </form>
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
            </ContentTemplate>
        </asp:UpdatePanel>

    </form>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <script src="Login/js/bootstrap.min.js"></script>
    <script src="Login/js/script.js"></script>
    <script src="../Scripts/notify.min.js"></script>
</body>
</html>
