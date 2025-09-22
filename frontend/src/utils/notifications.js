let notifySuccessFunc;
let notifyErrorFunc;

export function setNotifier(notifications) {
  notifySuccessFunc = notifications.notifySuccess;
  notifyErrorFunc = notifications.notifyError;
}

export function notifySuccess(message) {
  notifySuccessFunc?.(message);
}

export function notifyError(message) {
  notifyErrorFunc?.(message);
}

// Função para tratar o retorno de fetch
export async function handleResponse(response) {
  const data = await response.json().catch(() => null);

  if (!response.ok) {
    const errorMessage = data?.message || JSON.stringify(data) || "Erro desconhecido";
    notifyError(errorMessage);
    throw new Error(errorMessage);
  }

  if (data?.message) {
    notifySuccess(data.message);
  }

  return data;
}
