import {
  InvateDeleteQuery,
  InvateGetListQuery,
  InvateGetListReturn,
  InvatePostBody,
} from './invate-dto';
import {
  body,
  getHttpBackendClient,
  HttpBackendClient,
  queryParams,
  responseJson,
} from '@socnet/shared';

/** /api/invate */
class InvateRepository {
  constructor(private _httpClient: HttpBackendClient) {}

  /** POST /api/v1/invate */
  post(payload: InvatePostBody) {
    return this._httpClient
      .post('/api/v1/invate', body(payload))
      .then(responseJson<void>);
  }

  /** GET /api/v1/invate */
  get(query: InvateGetListQuery) {
    return this._httpClient
      .get('/api/v1/invate', queryParams(query))
      .then(responseJson<InvateGetListReturn>);
  }

  /** DELETE /api/v1/invate */
  delete(query: InvateDeleteQuery) {
    return this._httpClient
      .delete(`/api/v1/invate`, queryParams(query))
      .then(responseJson<void>);
  }
}

export const getInvateRepository = () =>
  new InvateRepository(getHttpBackendClient());
