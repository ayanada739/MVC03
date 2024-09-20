// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let element = document.getElementById("id");
element.AddEventListener("Keyup", () => {
    //Send Request To The Back End

    // Creating Our XMLHttpRequest object 
    let xhr = new XMLHttpRequest();

    // Making our connection  
    let url = `https://localhost:44363/Employees/Employees/Index?InputSearch=${element.value}`;
    xhr.open("Post", url, true);

    // function execute after request is successful 
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            console.log(this.responseText);
            //Render HTML Code To Response
        }
    }
    // Sending our request 
    xhr.send();
})
