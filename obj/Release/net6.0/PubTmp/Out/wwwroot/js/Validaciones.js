
// Función para validar Numeros
function validarEnteroEnCampo(identificadorDelCampo) {
    let field = document.getElementById(identificadorDelCampo);
    let valueInt = parseInt(field.value);
    if (!Number.isInteger(valueInt)) {
        return false;
    } else {
        field.value = valueInt;
        return true;
    }
}


//Función para validar correos
function validarEmail(valor) {
    if (/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3,4})+$/.test(valor)) {
        alert("La dirección de email " + valor + " es correcta.");
    } else {
        alert("La dirección de email es incorrecta.");
    }
}
