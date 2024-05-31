export function noNumbersOrSpecialCharacters(control: any) {
    const regex = /^[a-zA-Z\s]*$/;
    if (!regex.test(control.value)) {
        return { containsNumbersOrSpecialCharacters: true };
    }
    return null;
}