import { AxiosResponse } from 'axios';

export interface RequestParameters {
  params?: { [x: string]: unknown };
  body?: unknown;
}

/** Добавляет параметры в url запроса
 * @example {p1: 'v1', p2: 'v2'} => ?p1=v1&p2=v2
 * */
export const queryParams = <T extends NonNullable<unknown>>(
  params: T
): RequestParameters => ({
  params,
});

/** Добавляет body в запрос */
export const body = (value: unknown): RequestParameters => {
  return {
    body: value instanceof FormData ? value : JSON.stringify(value),
  };
};

export const mergeParams = (params: RequestParameters[]) => {
  let result: RequestParameters = {};
  for (const param of params) {
    result = {
      ...result,
      ...param,
    };
  }
  return result;
};

export const joinUrl = (baseUrl: string, url: string) => {
  return new URL(url, baseUrl).href;
};

export function parseJsonResponse<T>(data: unknown) {
  if (typeof data === 'string' && data) {
    try {
      return JSON.parse(data) as T;
    } catch {
      return undefined;
    }
  }
  return undefined;
}

/** Конвертирует ответ с бэкенда в объект */
export const responseJson = <T>(
  response: void | AxiosResponse<unknown, unknown>
) => {
  if (typeof response === 'undefined') {
    throw Error('Отсутствует ответ от сервера');
  }
  if (response.status >= 400 && response.status <= 599) {
    throw Error(`Ответ сервера ${response.status}`, {
      cause: response.data,
    });
  }
  return response ? (parseJsonResponse<T>(response.data) as T) : undefined;
};
