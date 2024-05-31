export function passwordStrengthValidator(control: any) {
    const password = control.value;
    const strongRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[\W]).{8,}$/;
    return strongRegex.test(password) ? null : { weakPassword: true };
}