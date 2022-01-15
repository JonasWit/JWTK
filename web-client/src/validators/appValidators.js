const minLength = min => {
    return input => input.length < min
        ? `Minimum ${min} znaków`
        : null;
};

const isTheSame = (check, whatShouldBeTheSame) => {
    return input => input !== check
        ? `${whatShouldBeTheSame} muszą być takie same`
        : null;
};

const isEmail = () => {
    const re = /\S+@\S+\.\S+/;
    return input => re.test(input)
        ? null
        : "Nieprawidłowy adres email";
}

export { minLength, isEmail, isTheSame };