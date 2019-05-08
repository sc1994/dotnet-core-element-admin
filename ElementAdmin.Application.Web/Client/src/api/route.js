import request from '@/utils/request'

export function getRouteRoles() {
  return request({
    url: '/routes/roles',
    method: 'get'
  })
}
