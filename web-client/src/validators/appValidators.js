const minLength = min => {
    return input => input.length < min
        ? `Minimum ${min} znaków`
        : null;
};

const isEmail = () => {
    const re = /\S+@\S+\.\S+/;
    return input => re.test(input)
        ? null
        : "Nieprawidłowy adres email";
}

export { minLength, isEmail };