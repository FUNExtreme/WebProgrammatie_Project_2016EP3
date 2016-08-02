<%@ Page Title="Register" Language="C#" MasterPageFile="~/WebForms/Shared/Layout.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="YouthLocationBooking.WebForms.Register" %>
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
                    <span>I am a: </span>
                    <asp:RadioButton id="registerAsParent" GroupName="registerAs" value="1" Checked="true" runat="server"  />Parent
                    <asp:RadioButton id="registerAsBabysitter" GroupName="registerAs" value="2" runat="server"  />Babysitter
                </div>
                <div class="form-group">
                    <i class="fa fa-pencil"></i>
                    <asp:TextBox id="registerFirstName" class="form-control__flat" placeholder="First Name" MaxLength="100" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ControlToValidate="registerFirstName" Text="Name is required!" Display="Dynamic" runat="server"/>
                    <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="registerFirstName" ValidationExpression = "^[\s\S]{1,100}$" runat="server" ErrorMessage="Minimum 1 and Maximum 100 characters required."></asp:RegularExpressionValidator>
                </div>
                <div class="form-group">
                    <i class="fa fa-pencil"></i>
                    <asp:TextBox id="registerLastName" class="form-control__flat" placeholder="Last Name" MaxLength="100" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ControlToValidate="registerLastName" Text="Name is required!" Display="Dynamic" runat="server"/>
                    <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="registerLastName" ValidationExpression = "^[\s\S]{1,100}$" runat="server" ErrorMessage="Minimum 1 and Maximum 100 characters required."></asp:RegularExpressionValidator>
                </div>

                <div class="form-group">
                    <i class="fa fa-envelope"></i>
                    <asp:TextBox id="registerEmail" class="form-control__flat" placeholder="Email Address" MaxLength="150" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ControlToValidate="registerEmail" Text="Email is required!" Display="Dynamic" runat="server"/>
                    <asp:RegularExpressionValidator Display="Dynamic" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="registerEmail" ErrorMessage="Invalid Email Format" />
                </div>

                <div class="form-group">
                    <i class="fa fa-location-arrow"></i>
                    <asp:TextBox id="registerAddressStreet" class="form-control__flat" placeholder="Street (Address)" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ControlToValidate="registerAddressStreet" Text="Street is required!" Display="Dynamic" runat="server"/>
                </div>

                <div class="form-group">
                    <i class="fa fa-location-arrow"></i>
                    <asp:TextBox id="registerAddressNumber" class="form-control__flat" placeholder="Number (Address)" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ControlToValidate="registerAddressNumber" Text="Number is required!" Display="Dynamic" runat="server"/>
                </div>

                <div class="form-group">
                    <i class="fa fa-location-arrow"></i>
                    <asp:TextBox id="registerAddressTown" class="form-control__flat" placeholder="Town (Address)" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ControlToValidate="registerAddressTown" Text="Town is required!" Display="Dynamic" runat="server"/>
                </div>

                <div class="form-group">
                    <i class="fa fa-location-arrow"></i>
                    <asp:TextBox id="registerAddressPostcode" class="form-control__flat" placeholder="Postcode (Address)" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ControlToValidate="registerAddressPostcode" Text="Postcode is required!" Display="Dynamic" runat="server" />
                    <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="registerAddressPostcode" ErrorMessage="Postcode should only contain numbers" ValidationExpression="^\d+$" runat="server" />
                </div>
            
                <div class="form-group">
                    <i class="fa fa-key"></i>
                    <asp:TextBox id="registerPassword" class="form-control__flat" type="password" placeholder="Password" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ControlToValidate="registerPassword" Text="Password is required!" Display="Dynamic" runat="server"/>
                </div>

                <div class="form-group">
                    <asp:Button id="registerSubmit" type="submit" class="btn btn-primary" Text="Register" runat="server" OnClick="registerSubmit_Click" />
                </div>
            </div>
        </div>
    </form>
</asp:Content>
