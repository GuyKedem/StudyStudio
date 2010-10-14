<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<StudyStudio.Models.Exercise.EditModel>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Edit</title>
</head>
<body>
    <div>
        <% using ( Html.BeginForm("Edit"))
           { %>
               <%= Html.TextAreaFor(e => e.Body) %>
               <input type="submit" value="Save"/>
        <% } %>
    </div>
</body>
</html>
