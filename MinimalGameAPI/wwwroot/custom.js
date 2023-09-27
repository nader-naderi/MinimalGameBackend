window.onload = function () {
    // Your token retrieval logic here
    var token = "my top secret key, please dont expose it to no body even the Colonel Buryat.";

    // Set the token in the Swagger UI
    const apiKey = "Bearer " + token;
    const input = document.querySelector("input[type='text'][placeholder='Bearer token']");
    if (input) {
        input.value = apiKey;
    }
};