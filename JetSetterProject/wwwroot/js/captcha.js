// Style and draw reCAPTCHA.
var renderRecaptcha = function () {
    grecaptcha.render('ReCaptchContainer', {
        'sitekey': '@ViewData["SiteKey"]',
        'callback': reCaptchaCallback,
        theme: 'light',    //light or dark
        type: 'image',    // image or audio
        size: 'normal'    //normal or compact
    });
};

var reCaptchaCallback = function (response) {
    // If reCAPTCHA is successful display it.
    if (response !== '') {
        jQuery('#lblMessage').css('color', 'green').html('Success');
        $(':input[type="submit"]').prop('disabled', false);
    }
};

// Check reCAPTCHA validation.
jQuery('button[type="button"]').click(function (e) {
    var message = 'Please checck the checkbox';
    if (typeof (grecaptcha) != 'undefined') {
        var response = grecaptcha.getResponse();
        (response.length === 0) ? (message = 'Captcha verification failed')
            : (message = 'Success!');
    }
    jQuery('#lblMessage').html(message);
    jQuery('#lblMessage').css('color',
        (message.toLowerCase() == 'success!') ? "green" : "red");
});

// Disable button when form loads.
$(document).ready(function () {
    $(':input[type="submit"]').prop('disabled', true);
});
