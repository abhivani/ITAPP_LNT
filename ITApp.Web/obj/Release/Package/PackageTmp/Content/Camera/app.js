// Set constraints for the video stream
var constraints = { video: { facingMode: "user" }, audio: false };
var track = null;

// Define constants
const cameraView = document.querySelector("#camera--view"),
    cameraOutput = document.querySelector("#camera--output"),
    cameraSensor = document.querySelector("#camera--sensor"),
    cameraTrigger = document.querySelector("#camera--trigger"),
    Url = $("#loader").data('request-url'),
    Url2 = $("#loader2").data('request-url'),
    Url3 = $("#loader3").data('request-url');
    

// Access the device camera and stream to cameraView
function cameraStart() {
    navigator.mediaDevices
        .getUserMedia(constraints)
        .then(function(stream) {
            track = stream.getTracks()[0];
            cameraView.srcObject = stream;
        })
        .catch(function(error) {
            console.error("Oops. Something is broken.", error);
        });
}

// Take a picture when cameraTrigger is tapped
cameraTrigger.onclick = function () {

    var From = $("#txtFrom").val();
    
    cameraSensor.width = cameraView.videoWidth; // prod
    cameraSensor.height = cameraView.videoHeight; // prod
    cameraSensor.getContext("2d").drawImage(cameraView, 0, 0); // prod
    
    //var ctx = cameraSensor.getContext("2d");  //dev
    //ctx.fillStyle = "#FF0000"; //dev
    //ctx.fillRect(0, 0, 150, 75); //dev

    cameraOutput.src = cameraSensor.toDataURL(); //prod
    cameraOutput.classList.add("taken"); 

    var dataURL = cameraSensor.toDataURL();
    $.ajax({
        type: "POST",
        url: Url,
        data: { imageData: dataURL },
        success: function (data) {
            
            if (data == "Success") {
                //window.setTimeout('window.location.href = ' + Url2 + '');/Transaction/GatePassForm?PopUp=Open
                if (From == "") {
                    window.location.href = Url2;
                }else
                {
                    window.location.href = Url3;
                }
            }
            else {
                $.toaster("Something went wrong!", 'Denied', 'danger');
            }
        }
    })
    // track.stop();
};

// Start the video stream when the window loads
window.addEventListener("load", cameraStart, false);


// Install ServiceWorker
if ('serviceWorker' in navigator) {
    console.log('CLIENT: service worker registration in progress.');
    navigator.serviceWorker.register('/portal/SystemApprovalApp/cameraapp/sw.js', { scope: ' ' }).then(function () {
        console.log('CLIENT: service worker registration complete.');
    }, function () {
        console.log('CLIENT: service worker registration failure.');
    });
} else {
    console.log('CLIENT: service worker is not supported.');
}

