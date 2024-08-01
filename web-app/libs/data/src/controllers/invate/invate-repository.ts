import { inject, injectable } from 'inversify';
import {
  HttpBackendClient,
  TYPES,
  DIContainer,
  body,
  queryParams,
  responseJson,
} from '@socnet/shared';
import {
  InvateDeleteQuery,
  InvateGetListQuery,
  InvateGetListReturn,
  InvatePostBody,
} from './invate-dto';

/** /api/invate */
@injectable()
export class InvateRepository {
  constructor(
    @inject(TYPES.HttpBackendClient) private _httpClient: HttpBackendClient
  ) {}

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
  DIContainer.get<InvateRepository>(TYPES.InvateRepository);
