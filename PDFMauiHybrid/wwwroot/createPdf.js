function createPdf() {
    var element = document.getElementById('content');
    var docTitle = document.getElementById('documentTitle').innerText;
    var filename = docTitle.replace(/\s+/g, '_') + '.pdf';
    var opt = {
        margin: 1,
        filename: filename,
        image: { type: 'jpeg', quality: 0.98 },
        html2canvas: { scale: 2 },
        jsPDF: { unit: 'in', format: 'legal', orientation: 'landscape' }
    };

    html2pdf().set(opt).from(element).toPdf().get('pdf').then(function (pdfObject) {
        var pdfOutput = pdfObject.output('datauristring');  // Generates a data URI in base64
        DotNet.invokeMethodAsync('PDFMauiHybrid', 'ReceivePdf', pdfOutput, filename);
    });
}