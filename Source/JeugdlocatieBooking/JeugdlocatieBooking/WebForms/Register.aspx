<%@ Page Title="Register" Language="C#" MasterPageFile="~/WebForms/Shared/Layout.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="YouthLocationBooking.Web.WebForms.Register" %>
<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ContentPlaceHolderID="navbreadcrumb" runat="server">
    <li><a href="@Url.Action("Index", "Home")">Home</a></li>
    <li class="active"><%: Page.Title %></li>
</asp:Content>

<asp:Content ContentPlaceHolderID="stylesheets" runat="server">
    <%: Styles.Render("~/Content/css/auth.css") %>
</asp:Content>

<asp:Content ContentPlaceHolderId="ContentPlaceHolder" runat="server">
    <form runat="server">
        <div class="content-group">
            <div class="inner">
                <div class="form-group">
                    <i class="fa fa-pencil"></i>
                    <asp:TextBox id="_registerFirstName" class="form-control__flat" placeholder="First Name" MaxLength="100" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ControlToValidate="_registerFirstName" Text="Name is required!" Display="Dynamic" runat="server"/>
                    <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="_registerFirstName" ValidationExpression = "^[\s\S]{1,100}$" runat="server" ErrorMessage="Minimum 1 and Maximum 100 characters required."></asp:RegularExpressionValidator>
                </div>

                <div class="form-group">
                    <i class="fa fa-pencil"></i>
                    <asp:TextBox id="_registerLastName" class="form-control__flat" placeholder="Last Name" MaxLength="100" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ControlToValidate="_registerLastName" Text="Name is required!" Display="Dynamic" runat="server"/>
                    <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="_registerLastName" ValidationExpression = "^[\s\S]{1,100}$" runat="server" ErrorMessage="Minimum 1 and Maximum 100 characters required."></asp:RegularExpressionValidator>
                </div>

                <div class="form-group">
                    <i class="fa fa-pencil"></i>
                    <asp:TextBox id="_registerPhoneNumber" class="form-control__flat" placeholder="Phone Number" MaxLength="100" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ControlToValidate="_registerPhoneNumber" Text="PHone number is required!" Display="Dynamic" runat="server"/>
                    <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="_registerPhoneNumber" ValidationExpression = "^[\s\S]{1,100}$" runat="server" ErrorMessage="Minimum 1 and Maximum 100 characters required."></asp:RegularExpressionValidator>
                </div>

                <div class="form-group">
                    <i class="fa fa-envelope"></i>
                    <asp:TextBox id="_registerEmail" class="form-control__flat" placeholder="Email Address" MaxLength="150" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ControlToValidate="_registerEmail" Text="Email is required!" Display="Dynamic" runat="server"/>
                    <asp:RegularExpressionValidator Display="Dynamic" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="_registerEmail" ErrorMessage="Invalid Email Format" />
                </div>
            
                <div class="form-group">
                    <i class="fa fa-key"></i>
                    <asp:TextBox id="_registerPassword" class="form-control__flat" type="password" placeholder="Password" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ControlToValidate="_registerPassword" Text="Password is required!" Display="Dynamic" runat="server"/>
                </div>

                <div class="form-group">
                    <asp:Button id="registerSubmit" type="submit" class="btn btn-primary" Text="Register" runat="server" OnClick="registerSubmit_Click" />
                </div>
            </div>
        </div>
    </form>
</asp:Content>
