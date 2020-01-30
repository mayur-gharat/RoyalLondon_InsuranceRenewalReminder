function ValidateFileUpload() {
    var fuData = document.getElementById('flupload');
    var FileUploadPath = fuData.value;

    if (FileUploadPath == '') {
        // There is no file selected
        alert("Please select input CSV file");
        return false;
    }
    else {
        var Extension = FileUploadPath.substring(FileUploadPath.lastIndexOf('.') + 1).toLowerCase();

        if (Extension == "csv" || Extension == "CSV" || Extension == "txt") {
            // Valid file type
        }
        else {
            // Not valid file type
            alert("Please select appropreate input file");
            return false;
        }
    }
}