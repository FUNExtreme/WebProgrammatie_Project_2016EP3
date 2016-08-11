<%@ Page Title="Register" Language="C#" MasterPageFile="~/WebForms/Shared/Layout.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="YouthLocationBooking.Web.WebForms.Register" %>
<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ContentPlaceHolderID="navbreadcrumb" runat="server">
    <li><a href="~/Home" runat="server">Home</a></li>
    <li class="active"><%: Page.Title %></li>
</asp:Content>

<asp:Content ContentPlaceHolderID="styles" runat="server">
    <%: Styles.Render("~/Assets/css/main/register.css") %>
</asp:Content>

<asp:Content ContentPlaceHolderId="ContentPlaceHolder" runat="server">
    <div class="page">
        <div class="row">
            <div class="shadowbox">
                <form runat="server">
                    <div class="form-group clearfix">
                        <i class="fa fa-user"></i>
                        <asp:Label CssClass="col-md-4 form-label" runat="server">Voornaam*</asp:Label>
                        <div class="col-md-8">
                            <asp:TextBox id="_registerFirstName" CssClass="form-control" placeholder="Voornaam" MaxLength="100" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator CssClass="form-error" ControlToValidate="_registerFirstName" Text="Voornaam is verplicht!" Display="Dynamic" runat="server"/>
                            <asp:RegularExpressionValidator CssClass="form-error" Display="Dynamic" ControlToValidate="_registerFirstName" ValidationExpression = "^[\s\S]{1,100}$" runat="server" ErrorMessage="Minimum 1 and Maximum 100 characters required."></asp:RegularExpressionValidator>
                        </div>
                    </div>

                    <div class="form-group clearfix">
                        <i class="fa fa-user"></i>
                        <asp:Label CssClass="col-md-4 form-label" runat="server">Achternaam*</asp:Label>
                        <div class="col-md-8">
                            <asp:TextBox id="_registerLastName" CssClass="form-control" placeholder="Achternaam" MaxLength="100" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator CssClass="form-error" ControlToValidate="_registerLastName" Text="Achternaam is verplicht!" Display="Dynamic" runat="server"/>
                            <asp:RegularExpressionValidator CssClass="form-error" Display="Dynamic" ControlToValidate="_registerLastName" ValidationExpression = "^[\s\S]{1,100}$" runat="server" ErrorMessage="Minimum 1 and Maximum 100 characters required."></asp:RegularExpressionValidator>
                        </div>
                    </div>

                    <div class="form-group clearfix">
                        <i class="fa fa-phone"></i>
                        <asp:Label CssClass="col-md-4 form-label" runat="server">Telefoonnummer*</asp:Label>
                        <div class="col-md-8">
                            <asp:TextBox id="_registerPhoneNumber" CssClass="form-control" placeholder="Telefoonnummer" MaxLength="100" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator CssClass="form-error" ControlToValidate="_registerPhoneNumber" Text="Telefoonnummer is verplicht!" Display="Dynamic" runat="server"/>
                            <asp:RegularExpressionValidator CssClass="form-error" Display="Dynamic" ControlToValidate="_registerPhoneNumber" ValidationExpression = "^[\s\S]{1,100}$" runat="server" ErrorMessage="Minimum 1 and Maximum 100 characters required."></asp:RegularExpressionValidator>
                        </div>
                    </div>

                    <div class="form-group clearfix">
                        <i class="fa fa-envelope"></i>
                        <asp:Label CssClass="col-md-4 form-label" runat="server">Email*</asp:Label>
                        <div class="col-md-8">
                            <asp:TextBox id="_registerEmail" CssClass="form-control" placeholder="Email" MaxLength="150" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator CssClass="form-error" ControlToValidate="_registerEmail" Text="Email is verplicht!" Display="Dynamic" runat="server"/>
                            <asp:RegularExpressionValidator CssClass="form-error" Display="Dynamic" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="_registerEmail" ErrorMessage="Ongeldig email formaat" />
                        </div>
                    </div>

                    <div class="form-group clearfix">
                        <i class="fa fa-key"></i>
                        <asp:Label CssClass="col-md-4 form-label" runat="server">Wachtwoord*</asp:Label>
                        <div class="col-md-8">
                            <asp:TextBox id="_registerPassword" CssClass="form-control" type="password" placeholder="Wachtwoord" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator CssClass="form-error" ControlToValidate="_registerPassword" Text="Wachtwoord is verplicht!" Display="Dynamic" runat="server"/>
                        </div>
                    </div>

                    <div class="form-group">
                        <asp:Button id="registerSubmit" type="submit" class="button" Text="Registreren" runat="server" OnClick="registerSubmit_Click" />
                    </div>
                </form>
            </div>
        </div>
    </div>
</asp:Content>
