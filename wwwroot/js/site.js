// Barbearia Dom Navalha - Scripts Interativos

document.addEventListener('DOMContentLoaded', function () {
    // Máscara de telefone brasileira automática (11) 98765-4321
    const telefoneInputs = document.querySelectorAll('input[type="tel"], #inputTelefone, #TelefoneCliente');
    
    telefoneInputs.forEach(input => {
        input.addEventListener('input', function (e) {
            let val = e.target.value.replace(/\D/g, '');
            if (val.length > 11) val = val.substring(0, 11);
            
            if (val.length > 6) {
                if (val.length === 11) {
                    val = `(${val.substring(0, 2)}) ${val.substring(2, 7)}-${val.substring(7)}`;
                } else {
                    val = `(${val.substring(0, 2)}) ${val.substring(2, 6)}-${val.substring(6)}`;
                }
            } else if (val.length > 2) {
                val = `(${val.substring(0, 2)}) ${val.substring(2)}`;
            } else if (val.length > 0) {
                val = `(${val}`;
            }
            e.target.value = val;
        });
    });

    // Animação suave para links de âncora
    document.querySelectorAll('a[href^="#"]').forEach(anchor => {
        anchor.addEventListener('click', function (e) {
            const targetId = this.getAttribute('href');
            if (targetId && targetId !== '#') {
                const targetElement = document.querySelector(targetId);
                if (targetElement) {
                    e.preventDefault();
                    targetElement.scrollIntoView({
                        behavior: 'smooth'
                    });
                }
            }
        });
    });
});
