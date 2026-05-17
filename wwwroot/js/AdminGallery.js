document.querySelectorAll('input[type="file"]').forEach(input => {
    input.addEventListener('change', async function () {
        const file = this.files[0];
        if (!file) return;

        const preview = document.getElementById(this.dataset.target);
        preview.src = URL.createObjectURL(file);

        const formData = new FormData();
        formData.append('image', file);
        formData.append('filename', this.dataset.filename);

        const token = document.querySelector('input[name="__RequestVerificationToken"]').value;

        const response = await fetch('/Home?handler=UploadImage', {
            method: 'POST',
            headers: { 'RequestVerificationToken': token },
            body: formData
        });

        if (response.ok) {
            alert('Image updated!');
        } else {
            alert('Upload failed. Status: ' + response.status);
        }
    });
});