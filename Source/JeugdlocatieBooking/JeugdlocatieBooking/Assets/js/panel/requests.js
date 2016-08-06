function openTab(event, tabId)
{
    var tabcontent = document.getElementsByClassName("requests-tab-content");
    for (var i = 0; i < tabcontent.length; i++)
    {
        tabcontent[i].style.display = "none";
    }

    var tablinks = document.getElementsByClassName("requests-tab");
    for (var i = 0; i < tablinks.length; i++)
    {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }

    document.getElementById(tabId).style.display = "block";
    event.currentTarget.className += " active";
}
