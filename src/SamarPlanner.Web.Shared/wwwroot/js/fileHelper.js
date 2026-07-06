window.fileUtils = {
    // ── Blob URL helpers ──
    createBlobUrl: function (base64, contentType) {
        var byteCharacters = atob(base64);
        var byteNumbers = new Array(byteCharacters.length);
        for (var i = 0; i < byteCharacters.length; i++) {
            byteNumbers[i] = byteCharacters.charCodeAt(i);
        }
        var byteArray = new Uint8Array(byteNumbers);
        var blob = new Blob([byteArray], { type: contentType });
        return URL.createObjectURL(blob);
    },

    revokeBlobUrl: function (url) {
        if (url) URL.revokeObjectURL(url);
    },

    revokeAll: function (urls) {
        if (urls && urls.length) {
            urls.forEach(function (u) { if (u) URL.revokeObjectURL(u); });
        }
    },

    downloadFromBase64: function (fileName, contentType, base64) {
        var link = document.createElement('a');
        link.href = 'data:' + contentType + ';base64,' + base64;
        link.download = fileName;
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
    },

    // ── Image compression ──
    compressImage: function (bytes, contentType, maxWidth, maxHeight, quality) {
        return new Promise(function (resolve, reject) {
            var blob = new Blob([bytes], { type: contentType });
            var url = URL.createObjectURL(blob);
            var img = new Image();

            img.onload = function () {
                var w = img.naturalWidth;
                var h = img.naturalHeight;

                if (w > maxWidth || h > maxHeight) {
                    var ratio = Math.min(maxWidth / w, maxHeight / h);
                    w = Math.round(w * ratio);
                    h = Math.round(h * ratio);
                }

                var canvas = document.createElement('canvas');
                canvas.width = w;
                canvas.height = h;
                var ctx = canvas.getContext('2d');
                ctx.drawImage(img, 0, 0, w, h);

                canvas.toBlob(function (resultBlob) {
                    URL.revokeObjectURL(url);
                    if (!resultBlob) { resolve(null); return; }
                    resultBlob.arrayBuffer().then(function (buffer) {
                        resolve(new Uint8Array(buffer));
                    });
                }, 'image/jpeg', quality);
            };

            img.onerror = function () {
                URL.revokeObjectURL(url);
                reject('Failed to load image');
            };

            img.src = url;
        });
    }
};