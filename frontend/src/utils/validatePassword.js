// src/utils/validatePassword.js
export const MIN_LENGTH = 8;

export function validatePassword(password) {
  const result = {
    length: password.length >= MIN_LENGTH,
    lowercase: /[a-z]/.test(password),
    uppercase: /[A-Z]/.test(password),
    number: /[0-9]/.test(password),
    special: /[^A-Za-z0-9]/.test(password), // qualquer coisa que não seja letra ou número
    noSpaces: !/\s/.test(password),
  };

  // conta quantos requisitos foram atendidos
  const score = Object.values(result).filter(Boolean).length;

  return {
    ...result,
    score,
    valid: score === 6, // todos os requisitos
  };
}
