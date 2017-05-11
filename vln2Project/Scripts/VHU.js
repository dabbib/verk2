function load() {
    if(selectedID == "Please open file to edit")
    {
        document.getElementById("sav-btn").disabled = true;
    }
    else
    {
        document.getElementById("sav-btn").disabled = false;
    }
};